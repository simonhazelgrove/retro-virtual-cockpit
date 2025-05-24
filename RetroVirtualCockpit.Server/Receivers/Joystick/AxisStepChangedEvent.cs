using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Server.Receivers.Joystick
{
    public class AxisStepChangedEvent : BaseJoystickAxisEvent, IJoystickEvent
    {
        public enum AxisChangeDirection
        {
            None, Up, Down
        }

        public int Steps { get; set; }

        public string UpGameAction { get; set; }

        public string DownGameAction { get; set; }

        public AxisChangeDirection ChangeDirection { get; set; }

        public AxisStepChangedEvent()
        {
        }

        public AxisStepChangedEvent(Axis axis, int steps, string upGameAction, string downGameAction) : base(axis)
        {
            Axis = axis;
            Steps = steps;
            UpGameAction = upGameAction;
            DownGameAction = downGameAction;
        }

        public override bool Evaluate(JoystickState previousState, JoystickState currentState)
        {
            var currentStep = GetAxisValue(currentState) / (ushort.MaxValue / Steps);
            var previousStep = GetAxisValue(previousState) / (ushort.MaxValue / Steps);

            if (currentStep > previousStep)
            {
                ChangeDirection = AxisChangeDirection.Up;
                return true;
            }
            else if (currentStep < previousStep)
            {
                ChangeDirection = AxisChangeDirection.Down;
                return true;
            }
            
            ChangeDirection = AxisChangeDirection.None;
            return false;
        }

        public override string GameAction
        {
            get
            {
                switch(ChangeDirection)
                {
                    case AxisChangeDirection.Up:
                        return UpGameAction;
                    case AxisChangeDirection.Down:
                        return DownGameAction;
                }

                return base.GameAction;
            }
        }
    }
}
