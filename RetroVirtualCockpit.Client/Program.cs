using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using RetroVirtualCockpit.Client.Data;
using WindowsInput.Native;
using RetroVirtualCockpit.Client.Helpers;
using WindowsInput;
using RetroVirtualCockpit.Client.Receivers;
using RetroVirtualCockpit.Client.Messages;
using RetroVirtualCockpit.Client.Receivers.Joystick;
using SharpDX.DirectInput;

// https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API/Writing_WebSocket_server

namespace RetroVirtualCockpit.Client
{
    class Program
    {
        const int PortNo = 6437;

        private static List<IInputReceiver> _receivers;

        private static MessageDispatcher _dispatcher;

        private static DirectInput _directInput;

        private static List<GameConfig> _gameConfigs = new List<GameConfig>
        {
            new GameConfig
            {
                Title = "F-16 Combat Pilot [ST/Amiga]",
                KeyMappings = new Dictionary<string, KeyMapping> {
                    { "Controls.Rudder.Left", new KeyMapping(VirtualKeyCode.VK_1) },
                    { "Controls.Rudder.Right", new KeyMapping(VirtualKeyCode.VK_3) },
                    { "Controls.Throttle.Up", new KeyMapping(VirtualKeyCode.OEM_PLUS) },
                    { "Controls.Throttle.Max", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.OEM_PLUS) },
                    { "Controls.Throttle.Down", new KeyMapping(VirtualKeyCode.OEM_MINUS) },
                    { "Controls.Throttle.Min", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.OEM_MINUS) },
                    { "Controls.Undercarriage", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.VK_U) },
                    { "Controls.Brakes.Air", new KeyMapping(VirtualKeyCode.BACK) },
                    { "Controls.Brakes.Wheel", new KeyMapping(VirtualKeyCode.VK_B) },
                    { "Controls.Eject", new KeyMapping(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_E) },
                    { "Stores.Jettison.Fuel", new KeyMapping(VirtualKeyCode.VK_J, VirtualKeyCode.VK_F) },
                    { "Stores.Jettison.All", new KeyMapping(VirtualKeyCode.VK_J, VirtualKeyCode.VK_A) },
                    { "MFD.DogfightMode", new KeyMapping(VirtualKeyCode.VK_D) },
                    { "MFD.1.Mode", new KeyMapping(VirtualKeyCode.F1) },
                    { "MFD.2.Mode", new KeyMapping(VirtualKeyCode.F2) },
                    { "MFD.3.Mode", new KeyMapping(VirtualKeyCode.F3) },
                    { "Target.Select", new KeyMapping(VirtualKeyCode.F9) },
                    { "Target.Designate", new KeyMapping(VirtualKeyCode.F10) },
                    { "Weapon.Next", new KeyMapping(VirtualKeyCode.TAB) },
                    { "Weapon.Prev", new KeyMapping(VirtualKeyCode.VK_Q) },
                    { "Weapon.Fire", new KeyMapping(VirtualKeyCode.SPACE) },
                    { "Defence.Flare", new KeyMapping(VirtualKeyCode.VK_F) },
                    { "Defence.Chaff", new KeyMapping(VirtualKeyCode.VK_C) },
                    { "UFCP.Mode", new KeyMapping(VirtualKeyCode.F5) },
                    { "UFCP.Channel", new KeyMapping(VirtualKeyCode.F6) },
                    { "UFCP.AutoPilot", new KeyMapping(VirtualKeyCode.F7) },
                    { "UFCP.ReccePod", new KeyMapping(VirtualKeyCode.F8) },
                    { "Radio.Callsign", new KeyMapping(VirtualKeyCode.VK_T) },
                    { "Radio.RequestGCA", new KeyMapping(VirtualKeyCode.VK_G) },
                    { "View.Head.Left", new KeyMapping(VirtualKeyCode.LEFT) },
                    { "View.Head.Right", new KeyMapping(VirtualKeyCode.RIGHT) },
                    { "View.Head.Rear", new KeyMapping(VirtualKeyCode.DOWN) },
                    { "View.Head.Front", new KeyMapping(VirtualKeyCode.UP) },
                    { "View.Head.Left.Lock", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.LEFT) },
                    { "View.Head.Right.Lock", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.RIGHT) },
                    { "View.Head.Rear.Lock", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.DOWN) },
                    { "View.Head.Front.Lock", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.UP) },
                    { "Game.Pause", new KeyMapping(VirtualKeyCode.VK_P) },
                    { "Game.Quit", new KeyMapping(VirtualKeyCode.CONTROL, VirtualKeyCode.ESCAPE) }
                }
            },
            new GameConfig
            {
                Title = "F-19 Stealth Fighter [ST/Amiga]",
                KeyMappings = new Dictionary<string, KeyMapping> {
                    { "HUD.Mode", new KeyMapping(VirtualKeyCode.F2) },
                    { "HUD.ILS", new KeyMapping(VirtualKeyCode.F9) },
                    { "MFD.L.Change", new KeyMapping(VirtualKeyCode.F3) },
                    { "MFD.L.Zoom.In", new KeyMapping(VirtualKeyCode.VK_Z) },
                    { "MFD.L.Zoom.Out", new KeyMapping(VirtualKeyCode.VK_X) },
                    { "MFD.R.Data", new KeyMapping(VirtualKeyCode.F4) },
                    { "MFD.R.Ordnance", new KeyMapping(VirtualKeyCode.F5) },
                    { "MFD.R.Damage", new KeyMapping(VirtualKeyCode.F6) },
                    { "MFD.R.Waypoints", new KeyMapping(VirtualKeyCode.F7) },
                    { "MFD.R.Mission", new KeyMapping(VirtualKeyCode.F10) },
                    { "Controls.Gear", new KeyMapping(VirtualKeyCode.VK_6) },
                    { "Controls.AutoPilot", new KeyMapping(VirtualKeyCode.VK_7) },
                    { "Controls.Flaps", new KeyMapping(VirtualKeyCode.VK_9) },
                    { "Controls.Brakes", new KeyMapping(VirtualKeyCode.VK_0) },
                    { "Controls.Throttle.Up", new KeyMapping(VirtualKeyCode.OEM_PLUS) },
                    { "Controls.Throttle.Max", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.OEM_PLUS) },
                    { "Controls.Throttle.Down", new KeyMapping(VirtualKeyCode.OEM_MINUS) },
                    { "Controls.Throttle.Min", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.OEM_MINUS) },
                    { "Controls.Eject", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.F10) },
                    { "Controls.Stick.Forward", new KeyMapping(VirtualKeyCode.UP) },
                    { "Controls.Stick.Back", new KeyMapping(VirtualKeyCode.DOWN) },
                    { "Controls.Stick.Left", new KeyMapping(VirtualKeyCode.LEFT) },
                    { "Controls.Stick.Right", new KeyMapping(VirtualKeyCode.RIGHT) },
                    { "Controls.StickSensitivity", new KeyMapping(VirtualKeyCode.INSERT) },
                    { "Defence.Flare", new KeyMapping(VirtualKeyCode.VK_1) },
                    { "Defence.Chaff", new KeyMapping(VirtualKeyCode.VK_2) },
                    { "Defence.IRJam", new KeyMapping(VirtualKeyCode.VK_3) },
                    { "Defence.ECM", new KeyMapping(VirtualKeyCode.VK_4) },
                    { "Defence.Decoy", new KeyMapping(VirtualKeyCode.VK_5) },
                    { "Weapon.Select", new KeyMapping(VirtualKeyCode.SPACE) },
                    { "Weapon.Drop", new KeyMapping(VirtualKeyCode.RETURN) },
                    { "Weapon.FireGun", new KeyMapping(VirtualKeyCode.BACK) },
                    { "Weapon.Bay", new KeyMapping(VirtualKeyCode.VK_8) },
                    { "Target.Select", new KeyMapping(VirtualKeyCode.VK_B) },
                    { "Target.Designate", new KeyMapping(VirtualKeyCode.VK_N) },
                    { "Camera.Left", new KeyMapping(VirtualKeyCode.VK_M) },
                    { "Camera.Right", new KeyMapping(VirtualKeyCode.OEM_COMMA) },
                    { "Camera.Rear", new KeyMapping(VirtualKeyCode.OEM_PERIOD) },
                    { "Camera.Front", new KeyMapping(VirtualKeyCode.OEM_2) }, // ? Key
                    { "View.Cockpit", new KeyMapping(VirtualKeyCode.F1) },
                    { "View.External.Slot", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.F1) },
                    { "View.External.ChasePlane", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.F2) },
                    { "View.External.Side", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.F3) },
                    { "View.External.Missile", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.F4) },
                    { "View.External.Tactical", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.F5) },
                    { "View.External.ReverseTactical", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.F6) },
                    { "View.Head.Left", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.VK_M) },
                    { "View.Head.Right", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.OEM_COMMA) },
                    { "View.Head.Rear", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.OEM_PERIOD) },
                    { "View.Head.Front", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.OEM_2) },  // ? character
                    { "View.Angle", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.VK_C) },
                    { "Game.Quit", new KeyMapping(VirtualKeyCode.MENU, VirtualKeyCode.VK_Q) },      // Alt + Q
                    { "Game.Resupply", new KeyMapping(VirtualKeyCode.MENU, VirtualKeyCode.VK_R) },  // Alt + R
                    { "Game.Pause", new KeyMapping(VirtualKeyCode.MENU, VirtualKeyCode.VK_P) },     // Alt + P
                    { "Game.Time.Accelerate", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.VK_Z) },
                    { "Game.Time.Normal", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.VK_X) },
                    { "Waypoint.Edit", new KeyMapping(VirtualKeyCode.F8) },
                    { "Waypoint.Reset", new KeyMapping(VirtualKeyCode.SHIFT, VirtualKeyCode.F8) },
                    { "Waypoint.Select.Next", new KeyMapping(VirtualKeyCode.NUMPAD3) },
                    { "Waypoint.Select.Previous", new KeyMapping(VirtualKeyCode.NUMPAD9) },
                    { "Waypoint.Move.Up", new KeyMapping(VirtualKeyCode.NUMPAD8) },
                    { "Waypoint.Move.Down", new KeyMapping(VirtualKeyCode.NUMPAD2) },
                    { "Waypoint.Move.Left", new KeyMapping(VirtualKeyCode.NUMPAD4) },
                    { "Waypoint.Move.Right", new KeyMapping(VirtualKeyCode.NUMPAD6) }
                },
                JoystickMappings = new List<IJoystickEvent>
                {
                    new ButtonValueChangedEvent(8, true, "Controls.Gear"),
                    new ButtonValueChangedEvent(9, true, "Controls.Gear"),
                    new ButtonValueChangedEvent(10, true, "Controls.Flaps"),
                    new ButtonValueChangedEvent(11, true, "Controls.Flaps"),
                    new ButtonValueChangedEvent(12, true, "Controls.Brakes"),
                    new ButtonValueChangedEvent(13, true, "Controls.Brakes"),
                    new AxisStepChangedEvent(Axis.Z, 10, "Controls.Throttle.Down", "Controls.Throttle.Up"),
                    new AxisValueChangedEvent(Axis.Z, ushort.MaxValue, "Controls.Throttle.Min"),
                    new AxisValueChangedEvent(Axis.Z, ushort.MinValue, "Controls.Throttle.Max"),
                    new ButtonValueChangedEvent(0, "Weapon.Bay"),
                    new ButtonValueChangedEvent(14, true, "Weapon.Drop"),
                    new ButtonValueChangedEvent(5, true, "Weapon.Select"),
                    new ButtonValueChangedEvent(2, true, new KeyboardMessage { MessageText = "Weapon.FireGun", Direction = KeyDirection.Down, DelayUntilKeyUp = null }),
                    new ButtonValueChangedEvent(2, false, new KeyboardMessage { MessageText = "Weapon.FireGun", Direction = KeyDirection.Up, DelayUntilKeyUp = null }),
                    new ButtonValueChangedEvent(22, true, "Camera.Left"),
                    new ButtonValueChangedEvent(20, true, "Camera.Right"),
                    new ButtonValueChangedEvent(21, true, "Camera.Rear"),
                    new ButtonValueChangedEvent(19, true, "Camera.Front"),
                    new ButtonValueChangedEvent(4, true, "Target.Select"),
                    new ButtonValueChangedEvent(3, true, "Target.Designate"),
                    new PovValueChangedEvent(0, -1, "View.Cockpit"),
                    new PovValueChangedEvent(0, 27000, "View.Head.Left"),
                    new PovValueChangedEvent(0, 9000, "View.Head.Right"),
                    new PovValueChangedEvent(0, 18000, "View.Head.Rear"),
                    new PovValueChangedEvent(0, 0, "View.External.ChasePlane"),
                    // TODO: Add more on the 45 degree angles?
                    new ButtonValueChangedEvent(26, true, "MFD.L.Change"),
                    new ButtonValueChangedEvent(24, true, new List<string> { "MFD.R.Data", "MFD.R.Ordnance", "MFD.R.Damage", "MFD.R.Waypoints", "MFD.R.Mission" }),
                    new ButtonValueChangedEvent(23, true, "MFD.L.Zoom.Out"),
                    new ButtonValueChangedEvent(25, true, "MFD.L.Zoom.In"),
                }
            },
            new GameConfig
            {
                Title = "Combat Lynx [Spectrum]",
                KeyMappings = new Dictionary<string, KeyMapping> {
                    { "Controls.Stick.Left", new KeyMapping(VirtualKeyCode.VK_5) },
                    { "Controls.Stick.Right", new KeyMapping(VirtualKeyCode.VK_8) },
                    { "Controls.Stick.Forward", new KeyMapping(VirtualKeyCode.VK_7) },
                    { "Controls.Stick.Back", new KeyMapping(VirtualKeyCode.VK_6) },
                    { "Controls.Forward", new KeyMapping(VirtualKeyCode.VK_3) },
                    { "Controls.Back", new KeyMapping(VirtualKeyCode.VK_4) },
                    { "Map.View", new KeyMapping(VirtualKeyCode.VK_M) },
                    { "Map.Up", new KeyMapping(VirtualKeyCode.VK_7) },
                    { "Map.Down", new KeyMapping(VirtualKeyCode.VK_6) },
                    { "Map.Left", new KeyMapping(VirtualKeyCode.VK_5) },
                    { "Map.Right", new KeyMapping(VirtualKeyCode.VK_8) },
                    { "Map.Up.Fast", new KeyMapping(VirtualKeyCode.VK_0, VirtualKeyCode.VK_7) },
                    { "Map.Down.Fast", new KeyMapping(VirtualKeyCode.VK_0, VirtualKeyCode.VK_6) },
                    { "Map.Left.Fast", new KeyMapping(VirtualKeyCode.VK_0, VirtualKeyCode.VK_5) },
                    { "Map.Right.Fast", new KeyMapping(VirtualKeyCode.VK_0, VirtualKeyCode.VK_8) },
                    { "Load.Less", new KeyMapping(VirtualKeyCode.VK_J) },
                    { "Load.More", new KeyMapping(VirtualKeyCode.VK_K) },
                    { "Load.Next", new KeyMapping(VirtualKeyCode.RETURN) },
                    { "Load.Exit", new KeyMapping(VirtualKeyCode.SPACE) },
                    { "Weapon.Prev", new KeyMapping(VirtualKeyCode.VK_2) },
                    { "Weapon.Next", new KeyMapping(VirtualKeyCode.VK_9) },
                    { "Weapon.Sight", new KeyMapping(VirtualKeyCode.RETURN) },
                    { "Weapon.Fire", new KeyMapping(VirtualKeyCode.VK_0) },
                    { "Msg.Read", new KeyMapping(VirtualKeyCode.VK_1) },
                    { "Msg.Base.Pos.0", new KeyMapping(VirtualKeyCode.LSHIFT, VirtualKeyCode.VK_P) },
                    { "Msg.Base.Pos.1", new KeyMapping(VirtualKeyCode.LSHIFT, VirtualKeyCode.VK_Q) },
                    { "Msg.Base.Pos.2", new KeyMapping(VirtualKeyCode.LSHIFT, VirtualKeyCode.VK_W) },
                    { "Msg.Base.Pos.3", new KeyMapping(VirtualKeyCode.LSHIFT, VirtualKeyCode.VK_E) },
                    { "Msg.Base.Pos.4", new KeyMapping(VirtualKeyCode.LSHIFT, VirtualKeyCode.VK_R) },
                    { "Msg.Base.Pos.5", new KeyMapping(VirtualKeyCode.LSHIFT, VirtualKeyCode.VK_T) },
                    { "Msg.Base.Status.0", new KeyMapping(VirtualKeyCode.RSHIFT, VirtualKeyCode.VK_P) },
                    { "Msg.Base.Status.1", new KeyMapping(VirtualKeyCode.RSHIFT, VirtualKeyCode.VK_Q) },
                    { "Msg.Base.Status.2", new KeyMapping(VirtualKeyCode.RSHIFT, VirtualKeyCode.VK_W) },
                    { "Msg.Base.Status.3", new KeyMapping(VirtualKeyCode.RSHIFT, VirtualKeyCode.VK_E) },
                    { "Msg.Base.Status.4", new KeyMapping(VirtualKeyCode.RSHIFT, VirtualKeyCode.VK_R) },
                    { "Msg.Base.Status.5", new KeyMapping(VirtualKeyCode.RSHIFT, VirtualKeyCode.VK_T) },
                    { "Game.Pause", new KeyMapping(VirtualKeyCode.VK_H) },
                },
                JoystickMappings = new List<IJoystickEvent>
                {
                    new PovValueChangedEvent(0, 18000, "Controls.Forward"),
                    new PovValueChangedEvent(0, 0, "Controls.Back"),
                    new ButtonValueChangedEvent(1, true, "Msg.Read"),
                    new ButtonValueChangedEvent(3, true, "Map.View"),
                    new ButtonValueChangedEvent(2, true, "Weapon.Sight"),
                    new ButtonValueChangedEvent(4, true, "Weapon.Prev"),
                    new ButtonValueChangedEvent(5, true, "Weapon.Next"),
                    new ButtonValueChangedEvent(4, true, "Load.Less"),
                    new ButtonValueChangedEvent(5, true, "Load.More"),
                    new ButtonValueChangedEvent(6, true, "Load.Exit"),
                    new ButtonValueChangedEvent(0, true, "Weapon.Fire"),
                    new ButtonValueChangedEvent(7, true, "Game.Pause"),
                }
            },
        };

        static void Main(string[] args)
        {
            _receivers = new List<IInputReceiver>();
            _directInput = new DirectInput();

            GetJoysticks();

            var server = StartWebClientServer();
            var messages = new List<Message>();

            _dispatcher = new MessageDispatcher(_gameConfigs, new InputSimulator());

            while (true)
            {
                // Check for web client connections
                if (server.Pending())
                {
                    AcceptWebClientConnection(server);
                }

                messages.Clear();

                // Read all inputs
                foreach (var receiver in _receivers)
                {
                    receiver.ReceiveInput();
                    messages.AddRange(receiver.CollectMessages());
                }

                // Process messages
                messages.ForEach(m => _dispatcher.Dispatch(m, SetSelectedGameConfig));
            }
        }

        private static void GetJoysticks()
        {
            var devices = GetAttachedDevices();

            foreach (var deviceInstance in devices)
            {
                var _joystick = new Joystick(_directInput, deviceInstance.InstanceGuid);
                Console.WriteLine($"Joystick detected: {deviceInstance.InstanceName}");
                var receiver = new JoystickToKeyboardReceiver(_joystick);
                _receivers.Add(receiver);
            }
        }

        private static List<DeviceInstance> GetAttachedDevices()
        {
            var devices = new List<DeviceInstance>();

            devices.AddRange(_directInput.GetDevices(DeviceType.ControlDevice, DeviceEnumerationFlags.AttachedOnly));
            devices.AddRange(_directInput.GetDevices(DeviceType.Driving, DeviceEnumerationFlags.AttachedOnly));
            devices.AddRange(_directInput.GetDevices(DeviceType.FirstPerson, DeviceEnumerationFlags.AttachedOnly));
            devices.AddRange(_directInput.GetDevices(DeviceType.Flight, DeviceEnumerationFlags.AttachedOnly));
            devices.AddRange(_directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly));
            devices.AddRange(_directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly));

            return devices;
        }

        private static void SetSelectedGameConfig(GameConfig config)
        {
            foreach(var receiver in _receivers)
            {
                if (receiver is JoystickReceiver)
                {
                    (receiver as JoystickReceiver).SetEvents(config.JoystickMappings);
                }
            }
        }

        private static TcpListener StartWebClientServer()
        {
            var ipAddress = GetLocalIPv4(NetworkInterfaceType.Ethernet);
            var server = new TcpListener(IPAddress.Parse(ipAddress), PortNo);

            server.Start();

            Console.WriteLine($"Server has started on {ipAddress}:{PortNo}.");

            var code = IpAddressEncoder.Encode(ipAddress);
            Console.WriteLine($"Enter this connection key at Retro Virtual Cockpit website: '{code}'.");

            Console.WriteLine("Waiting for a connection...");
            return server;
        }

        private static void AcceptWebClientConnection(TcpListener server)
        {
            var client = server.AcceptTcpClient();
            Console.WriteLine("A client connected.");

            var receiver = new WebClientReceiver(client);

            _receivers.Add(receiver);
        }

        private static string GetLocalIPv4(NetworkInterfaceType networkInterfaceType)
        {
            var output = "";
            foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == networkInterfaceType && item.OperationalStatus == OperationalStatus.Up)
                {
                    var adapterProperties = item.GetIPProperties();

                    if (adapterProperties.GatewayAddresses.FirstOrDefault() != null)
                    {
                        foreach (var ip in adapterProperties.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                output = ip.Address.ToString();
                            }
                        }
                    }
                }
            }

            return output;
        }
    }
}
