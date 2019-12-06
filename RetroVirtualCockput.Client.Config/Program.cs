using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RetroVirtualCockput.Client.Config
{
    class Program
    {
        private static DirectInput _directInput;

        private static List<Joystick> _attachedSticks;

        private static Dictionary<Joystick, JoystickState> _attachedStickStates;

        static void Main(string[] args)
        {
            Console.WriteLine("RetroVirtualCockpit Configuration");

            _directInput = new DirectInput();
            _attachedSticks = new List<Joystick>();
            _attachedStickStates = new Dictionary<Joystick, JoystickState>();

            while (true)
            {
                CheckForAttachedDevices();
                CheckForStateChanges();
            }
        }

        private static void CheckForStateChanges()
        {
            foreach(var stick in _attachedSticks)
            {
                CheckForStateChanges(stick);
            }
        }

        private static void CheckForStateChanges(Joystick stick)
        {
            stick.Acquire();

            var currentState = stick.GetCurrentState();

            if (_attachedStickStates[stick] != null)
            {
                JoystickStateComparer.Compare(stick, currentState, _attachedStickStates[stick]);
            }

            // Store current state
            _attachedStickStates[stick] = currentState;
        }

        private static void CheckForAttachedDevices()
        {
            var detectedStickIds = new List<Guid>();

            var devices = GetAttachedDevices();

            // Detect all attached sticks
            foreach (var deviceInstance in devices)
            {
                CheckForNewAttachedDevice(detectedStickIds, deviceInstance);
            }

            // Now check for any sticks that have been unplugged
            var detachedSticks = _attachedSticks.Where(s => !detectedStickIds.Contains(s.Information.InstanceGuid)).ToList();

            foreach (var detachedStick in detachedSticks)
            {
                Console.WriteLine($"Joytick unplugged: {detachedStick.Information.InstanceName} (Type: {detachedStick.Capabilities.Type}, SubType: {detachedStick.Capabilities.Subtype})");
                RemoveAttachedStick(detachedStick);
            }
        }

        private static void CheckForNewAttachedDevice(List<Guid> detectedStickIds, DeviceInstance deviceInstance)
        {
            try
            {
                var joystick = new Joystick(_directInput, deviceInstance.InstanceGuid);
                detectedStickIds.Add(joystick.Information.InstanceGuid);

                // Check for any new sticks
                if (!_attachedSticks.Any(j => j.Information.InstanceGuid == joystick.Information.InstanceGuid))
                {
                    Console.WriteLine($"Joystick detected: {joystick.Information.InstanceName} (Type: {joystick.Capabilities.Type}, SubType: {joystick.Capabilities.Subtype})");
                    AddAttachedStick(joystick);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void AddAttachedStick(Joystick joystick)
        {
            _attachedSticks.Add(joystick);
            _attachedStickStates.Add(joystick, null);
        }

        private static void RemoveAttachedStick(Joystick joystick)
        {
            _attachedStickStates.Remove(joystick);
            _attachedSticks.Remove(joystick);
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
    }
}
