using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using RetroVirtualCockpit.Client.Data;
using WindowsInput.Native;
using RetroVirtualCockpit.Client.Helpers;
using WindowsInput;

// https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API/Writing_WebSocket_server

namespace RetroVirtualCockpit.Client
{
    class Program
    {
        const int PortNo = 6437;

        private static List<GameConfig> gameConfigs = new List<GameConfig>
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
                    { "MFD.L.Zoom.In", new KeyMapping(VirtualKeyCode.VK_X) },
                    { "MFD.L.Zoom.Out", new KeyMapping(VirtualKeyCode.VK_Z) },
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
                }
            }
        };

        private static MessageReceiver _messageReceiver;

        static void Main(string[] args)
        {
            var ipAddress = GetLocalIPv4(NetworkInterfaceType.Ethernet);
            var server = new TcpListener(IPAddress.Parse(ipAddress), PortNo);

            server.Start();

            Console.WriteLine($"Server has started on {ipAddress}:{PortNo}.");

            var code = IpAddressEncoder.Encode(ipAddress);
            Console.WriteLine($"Enter this connection key at Retro Virtual Cockpit website: '{code}'.");

            Console.WriteLine("Waiting for a connection...");

            _messageReceiver = new MessageReceiver(gameConfigs, new InputSimulator());

            while (true) // Add your exit flag here
            {
                var client = server.AcceptTcpClient();
                Console.WriteLine("A client connected.");
                ThreadPool.QueueUserWorkItem(_messageReceiver.ThreadedClientListener, client);
            }
        }

        internal static string GetLocalIPv4(NetworkInterfaceType networkInterfaceType)
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
