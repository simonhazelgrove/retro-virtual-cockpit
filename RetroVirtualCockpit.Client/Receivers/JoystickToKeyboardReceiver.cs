using System.Collections.Generic;
using RetroVirtualCockpit.Client.Messages;
using RetroVirtualCockpit.Client.Receivers.Joystick;

namespace RetroVirtualCockpit.Client.Receivers
{
    public class JoystickToKeyboardReceiver : JoystickReceiver
    {
        private Dictionary<Axis, string> _lastAxisMessage;

        public JoystickToKeyboardReceiver(SharpDX.DirectInput.Joystick joystick) : base(joystick)
        {
            _lastAxisMessage = new Dictionary<Axis, string>
            {
                { Axis.X, null },
                { Axis.Y, null },
            };
        }

        public override void ReceiveInput()
        {
            base.ReceiveInput();

            if (CurrentState == null)
            {
                return;
            }

            if (CurrentState.X < DeadZoneStart)
            {
                KeyDownForAxis(Axis.X, "Controls.Stick.Left");
            }
            else if (CurrentState.X > DeadZoneEnd)
            {
                KeyDownForAxis(Axis.X, "Controls.Stick.Right");
            }
            else
            {
                KeyUpForAxis(Axis.X);
            }

            if (CurrentState.Y < DeadZoneStart)
            {
                KeyDownForAxis(Axis.Y, "Controls.Stick.Forward");
            }
            else if (CurrentState.Y > DeadZoneEnd)
            {
                KeyDownForAxis(Axis.Y, "Controls.Stick.Back");
            }
            else
            {
                KeyUpForAxis(Axis.Y);
            }
        }

        private void KeyUpForAxis(Axis axis)
        {
            if (!string.IsNullOrEmpty(_lastAxisMessage[axis]))
            {
                _messages.Add(new KeyboardMessage { MessageText = _lastAxisMessage[axis], Direction = KeyDirection.Up });
                _lastAxisMessage[axis] = null;
            }
        }

        private void KeyDownForAxis(Axis axis, string message)
        {
            if (_lastAxisMessage[axis] != message)
            {
                // Key up previous message
                KeyUpForAxis(axis);

                _messages.Add(new KeyboardMessage { MessageText = message, Direction = KeyDirection.Down, DelayUntilKeyUp = null });
                _lastAxisMessage[axis] = message;
            }
        }
    }
}
