using RetroVirtualCockpit.Client.Messages;
using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Client.Receivers.Joystick
{
    public class PovValueChangedEvent : BaseJoystickEvent, IJoystickEvent
    {
        public int PovIndex { get; set; }

        public int Value { get; set; }

        public PovValueChangedEvent(int index, int value, string messageText) : base(messageText)
        {
            PovIndex = index;
            Value = value;
        }

        public Message Evaluate(JoystickState previousState, JoystickState currentState)
        {
            if (currentState.PointOfViewControllers[PovIndex] == Value && previousState.PointOfViewControllers[PovIndex] != Value)
            {
                return GetMessage();
            }

            return null;
        }
    }
}
