using RetroVirtualCockpit.Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using WindowsInput;
using WindowsInput.Native;

namespace RetroVirtualCockpit.Client
{
    public class MessageReceiver
    {
        private readonly List<GameConfig> _gameConfigs;

        private readonly Regex _getRegex = new Regex("^GET");

        private readonly InputSimulator _inputSimulator; 

        private GameConfig _selectedGameConfig = null;

        public MessageReceiver(List<GameConfig> gameConfigs, InputSimulator inputSimulator)
        {
            _gameConfigs = gameConfigs;
            _inputSimulator = inputSimulator;
        }

        public void ThreadedClientListener(object obj)
        {
            var client = (TcpClient)obj;
            var stream = client.GetStream();

            while (true)
            {
                while (!stream.DataAvailable) ;

                var bytes = new byte[client.Available];

                stream.Read(bytes, 0, bytes.Length);

                //translate bytes of request to string
                var data = Encoding.UTF8.GetString(bytes);

                if (_getRegex.IsMatch(data))
                {
                    SocketHandshake(data, stream);
                }
                else
                {
                    HandleSocketMessage(bytes);
                }
            }
        }

        private void HandleSocketMessage(byte[] bytes)
        {
            var encoded = bytes.Skip(6).ToArray();
            var key = bytes.Skip(2).Take(4).ToArray();
            var decoded = new byte[encoded.Length];

            for (var i = 0; i < encoded.Length; i++)
            {
                decoded[i] = (byte)(encoded[i] ^ key[i % 4]);
            }

            var message = Encoding.UTF8.GetString(decoded);
            InterpretMessage(message);
        }

        public void InterpretMessage(string message)
        {
            if (message.StartsWith("SetConfig:"))
            {
                var title = message.Substring("SetConfig:".Length);
                _selectedGameConfig = _gameConfigs.FirstOrDefault(c => c.Title == title);
                var logMessage = _selectedGameConfig == null ? $"Unknown game config {title}" : $"Selected game config {title}";
                Console.WriteLine(logMessage);
            }
            else if (_selectedGameConfig == null)
            {
                Console.WriteLine($"No game config selected, ignoring message '{message}'");
            }
            else
            {
                Console.Write(message + ": ");

                var modifier = VirtualKeyCode.NONAME;
                var key = VirtualKeyCode.NONAME;

                KeyMapping keyMapping;
                if (_selectedGameConfig.KeyMappings.TryGetValue(message, out keyMapping))
                {
                    key = keyMapping.KeyCode;

                    if (keyMapping.ModifierKeyCode.HasValue)
                    {
                        modifier = keyMapping.ModifierKeyCode.Value;
                    }
                }

                MakeKeyPress(modifier, key);
            }
        }

        private void MakeKeyPress(VirtualKeyCode modifier, VirtualKeyCode key)
        {
            if (key != VirtualKeyCode.NONAME)
            {
                if (modifier == VirtualKeyCode.NONAME)
                {
                    //inputSimulator.Keyboard.KeyPress(key);

                    // Instead of keypress, F-16 Combat Pilot needs keys to be held down for a bit longer - so keydown followed by a keyup sometime later
                    _inputSimulator.Keyboard.KeyDown(key);
                    Console.WriteLine($"{key} down");

                    var timer = new System.Timers.Timer
                    {
                        Interval = 200
                    };
                    timer.Elapsed += (o, e) =>
                    {
                        _inputSimulator.Keyboard.KeyUp(key);
                        Console.WriteLine($"{key} up");
                        timer.Stop();
                    };
                    timer.Start();
                }
                else
                {
                    //inputSimulator.Keyboard.ModifiedKeyStroke(new[] { modifier }, key);
                    _inputSimulator.Keyboard.KeyDown(modifier);
                    _inputSimulator.Keyboard.KeyPress(key);
                    _inputSimulator.Keyboard.KeyUp(modifier);
                    Console.WriteLine($"{modifier} + {key}");
                }
            }
            else
            {
                Console.WriteLine("*** UNKNOWN ***");
            }
        }

        private void SocketHandshake(string data, NetworkStream stream)
        {
            byte[] response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + Environment.NewLine
                                                     + "Connection: Upgrade" + Environment.NewLine
                                                     + "Upgrade: websocket" + Environment.NewLine
                                                     + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
                                                         SHA1.Create().ComputeHash(
                                                             Encoding.UTF8.GetBytes(
                                                                 new Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1]
                                                                     .Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                                                                 )
                                                             )
                                                         ) + Environment.NewLine
                                                     + Environment.NewLine);

            stream.Write(response, 0, response.Length);
        }
    }
}
