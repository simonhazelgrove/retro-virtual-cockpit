using RetroVirtualCockpit.Client.Messages;
using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Client.Receivers.Joystick
{
    public class AxisStepChangedEvent : BaseJoystickAxisEvent, IJoystickEvent
    {
        public int Steps { get; set; }

        public string UpMessage { get; set; }

        public string DownMessage { get; set; }

        public AxisStepChangedEvent(Axis axis, int steps, string upMessage, string downMessage) : base(axis)
        {
            Axis = axis;
            Steps = steps;
            UpMessage = upMessage;
            DownMessage = downMessage;
        }

        public Message Evaluate(JoystickState previousState, JoystickState currentState)
        {
            var currentStep = GetAxisValue(currentState) / (ushort.MaxValue / Steps);
            var previousStep = GetAxisValue(previousState) / (ushort.MaxValue / Steps);

            if (currentStep > previousStep)
            {
                return new KeyboardMessage { MessageText = UpMessage };
            }
            else if (currentStep < previousStep)
            {
                return new KeyboardMessage { MessageText = DownMessage };
            }

            return null;
        }
    }
}
