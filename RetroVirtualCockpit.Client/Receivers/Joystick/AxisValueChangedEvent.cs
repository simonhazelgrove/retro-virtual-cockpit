using RetroVirtualCockpit.Client.Messages;
using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Client.Receivers.Joystick
{
    public class AxisValueChangedEvent : BaseJoystickAxisEvent, IJoystickEvent
    {
        public ushort Value { get; set; }

        public AxisValueChangedEvent(Axis axis, ushort value, string messageText) : base(axis, messageText)
        {
            Value = value;
        }

        public Message Evaluate(JoystickState previousState, JoystickState currentState)
        {
            var currentValue = GetAxisValue(currentState);

            if (Value == currentValue && Value != GetAxisValue(previousState))
            {
                return GetMessage();
            }

            return null;
        }
    }
}
