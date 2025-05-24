using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace RetroVirtualCockpit.Server.Receivers
{
    public class WebClientReceiver : IInputReceiver
    {
        private readonly Regex _getRegex = new Regex("^GET");

        private readonly List<string> _messages;

        private readonly object _lock;

        private readonly TcpClient _client;

        private readonly NetworkStream _stream;

        public WebClientReceiver(TcpClient client)
        {
            _messages = new List<string>();
            _lock = new object();
            _client = client;
            _stream = _client.GetStream();
        }

        public void ReceiveInput()
        {
            if (_stream.DataAvailable)
            { 
                var bytes = new byte[_client.Available];

                _stream.Read(bytes, 0, bytes.Length);

                //translate bytes of request to string
                var data = Encoding.UTF8.GetString(bytes);

                if (_getRegex.IsMatch(data))
                {
                    SocketHandshake(data, _stream);
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

            lock (_lock)
            {
                _messages.Add(message);
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

        public List<string> CollectMessages()
        {
            lock (_lock)
            {
                var messages = new List<string>(_messages);
                _messages.Clear();
                return messages;
            }
        }
    }
}
