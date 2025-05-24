using RetroVirtualCockpit.Server.Receivers.Joystick;
using SharpDX.DirectInput;
using System.Collections.Generic;

namespace RetroVirtualCockpit.Server.Receivers
{
    public class JoystickReceiver : IInputReceiver
    {
        private const double HalfUShort = ushort.MaxValue / 2;

        public const double DeadZoneStart = HalfUShort - ushort.MaxValue / 10;

        public const double DeadZoneEnd = HalfUShort + ushort.MaxValue / 10;

        private readonly SharpDX.DirectInput.Joystick _joystick;

        public JoystickState CurrentState;

        public JoystickState PreviousState;

        protected readonly List<string> _messages;

        protected readonly object _lock;

        private List<IJoystickEvent> _events;

        public JoystickReceiver(SharpDX.DirectInput.Joystick joystick)
        {
            _messages = new List<string>();
            _lock = new object();
            _joystick = joystick;
        }

        public void SetEvents(List<IJoystickEvent> events)
        {
            _events = events;
        }

        protected void ReadJoystickState()
        {
            if (_joystick != null)
            {
                _joystick.Acquire();
                PreviousState = CurrentState;
                CurrentState = _joystick.GetCurrentState();
            }
        }

        public virtual void ReceiveInput()
        {
            ReadJoystickState();

            if (CurrentState != null && PreviousState != null && _events != null)
            {
                foreach (var stickEvent in _events)
                {
                    if (stickEvent.Evaluate(PreviousState, CurrentState))
                    {
                        _messages.Add(stickEvent.GameAction);
                    }
                }
            }
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
