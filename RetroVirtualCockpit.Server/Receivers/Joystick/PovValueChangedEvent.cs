using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Server.Receivers.Joystick
{
    public class PovValueChangedEvent : BaseJoystickEvent, IJoystickEvent
    {
        public int PovIndex { get; set; }

        public int Value { get; set; }

        public PovValueChangedEvent()
        {
        }

        public PovValueChangedEvent(int index, int value, string gameAction) : base(gameAction)
        {
            PovIndex = index;
            Value = value;
        }

        public override bool Evaluate(JoystickState previousState, JoystickState currentState)
        {
            return (currentState.PointOfViewControllers[PovIndex] == Value && previousState.PointOfViewControllers[PovIndex] != Value);
        }
    }
}
