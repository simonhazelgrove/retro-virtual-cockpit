using RetroVirtualCockpit.Client.Messages;
using RetroVirtualCockpit.Client.Receivers.Joystick;
using SharpDX.DirectInput;
using System.Collections.Generic;

namespace RetroVirtualCockpit.Client.Receivers
{
    public abstract class JoystickReceiver : IInputReceiver
    {
        private const double HalfUShort = ushort.MaxValue / 2;

        public const double DeadZoneStart = HalfUShort - ushort.MaxValue / 10;

        public const double DeadZoneEnd = HalfUShort + ushort.MaxValue / 10;

        private readonly SharpDX.DirectInput.Joystick _joystick;

        public JoystickState CurrentState;

        public JoystickState PreviousState;

        protected readonly List<Message> _messages;

        protected readonly object _lock;

        private List<IJoystickEvent> _events;

        public JoystickReceiver(SharpDX.DirectInput.Joystick joystick)
        {
            _messages = new List<Message>();
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
                    var message = stickEvent.Evaluate(PreviousState, CurrentState);

                    if (message != null)
                    {
                        _messages.Add(message);
                    }
                }
            }
        }

        public List<Message> CollectMessages()
        {
            lock (_lock)
            {
                var messages = new List<Message>(_messages);
                _messages.Clear();
                return messages;
            }
        }
    }
}
