using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Server.Receivers.Mouse
{
    public class MouseWheelEvent : BaseMouseEvent, IMouseEvent
    {
        public string UpGameAction { get; set; }    

        public string DownGameAction { get; set; }

        public MouseWheelEvent()
        {
        }

        public MouseWheelEvent(string upGameAction, string downGameAction) : base()
        {
            UpGameAction = upGameAction;
            DownGameAction = downGameAction;
        }    

        public override bool Evaluate(MouseState previousState, MouseState currentState)
        {
            var currentValue = currentState.Z;

            if (currentState.Z < previousState.Z && currentState.Z < 0)
            {
                GameAction = DownGameAction;
                return true;
            }
            else if (currentState.Z > previousState.Z && currentState.Z > 0)
            {
                GameAction = UpGameAction;
                return true;
            }

            return false;
        }
    }
}