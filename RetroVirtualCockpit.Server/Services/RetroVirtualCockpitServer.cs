using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.Extensions.DependencyInjection;
using RetroVirtualCockpit.Server.Data;
using RetroVirtualCockpit.Server.Helpers;
using RetroVirtualCockpit.Server.Receivers;
using SharpDX.DirectInput;

// https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API/Writing_WebSocket_server

namespace RetroVirtualCockpit.Server.Services
{
    public class RetroVirtualCockpitServer : IRetroVirtualCockpitServer
    {
        public ServiceLifetime Lifetime => ServiceLifetime.Singleton;
        const int PortNo = 6437;

        private List<IInputReceiver> _receivers;

        private IMessageDispatcher _dispatcher;

        private DirectInput _directInput;

        public RetroVirtualCockpitServer(IMessageDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Start()
        {
            _receivers = new List<IInputReceiver>();
            _directInput = new DirectInput();

            GetJoysticks();

            var server = StartWebClientServer();
            var messages = new List<string>();

            while (true)
            {
                System.Threading.Thread.Sleep(100);

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

        private void GetJoysticks()
        {
            var devices = GetAttachedDevices();

            foreach (var deviceInstance in devices)
            {
                if (deviceInstance.Type == DeviceType.Mouse)
                {
                    var mouse = new SharpDX.DirectInput.Mouse(_directInput);
                    Console.WriteLine($"Mouse detected: {deviceInstance.InstanceName}");
                    var receiver = new MouseReceiver(mouse);
                    _receivers.Add(receiver);
                }
                else
                {
                    var joystick = new Joystick(_directInput, deviceInstance.InstanceGuid);
                    Console.WriteLine($"Joystick detected: {deviceInstance.InstanceName}");
                    var receiver = new JoystickReceiver(joystick);
                    _receivers.Add(receiver);
                }
            }
        }

        private List<DeviceInstance> GetAttachedDevices()
        {
            var devices = new List<DeviceInstance>();

            devices.AddRange(_directInput.GetDevices(DeviceType.ControlDevice, DeviceEnumerationFlags.AttachedOnly));
            devices.AddRange(_directInput.GetDevices(DeviceType.Driving, DeviceEnumerationFlags.AttachedOnly));
            devices.AddRange(_directInput.GetDevices(DeviceType.FirstPerson, DeviceEnumerationFlags.AttachedOnly));
            devices.AddRange(_directInput.GetDevices(DeviceType.Flight, DeviceEnumerationFlags.AttachedOnly));
            devices.AddRange(_directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly));
            devices.AddRange(_directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly));
            devices.AddRange(_directInput.GetDevices(DeviceType.Mouse, DeviceEnumerationFlags.AttachedOnly));

            return devices;
        }

        private void SetSelectedGameConfig(GameConfig config)
        {
            foreach(var receiver in _receivers)
            {
                if (receiver is JoystickReceiver)
                {
                    (receiver as JoystickReceiver).SetEvents(config.JoystickMappings);
                }
                else if (receiver is MouseReceiver)
                {
                    (receiver as MouseReceiver).SetEvents(config.MouseMappings);
                }
            }
        }

        private TcpListener StartWebClientServer()
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

        private void AcceptWebClientConnection(TcpListener server)
        {
            var client = server.AcceptTcpClient();
            Console.WriteLine("A client connected.");

            var receiver = new WebClientReceiver(client);

            _receivers.Add(receiver);
        }

        private string GetLocalIPv4(NetworkInterfaceType networkInterfaceType)
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