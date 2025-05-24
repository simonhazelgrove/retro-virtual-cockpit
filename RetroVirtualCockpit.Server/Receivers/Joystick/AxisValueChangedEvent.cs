using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Server.Receivers.Joystick
{
    public class AxisValueChangedEvent : BaseJoystickAxisEvent, IJoystickEvent
    {
        public ushort Value { get; set; }

        public AxisValueChangedEvent()
        {
        }

        public AxisValueChangedEvent(Axis axis, ushort value, string gameAction) : base(axis, gameAction)
        {
            Value = value;
        }

        public override bool Evaluate(JoystickState previousState, JoystickState currentState)
        {
            var currentValue = GetAxisValue(currentState);

            return (Value == currentValue && Value != GetAxisValue(previousState));
        }
    }
}
