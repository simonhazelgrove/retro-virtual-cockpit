using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Server.Receivers.Mouse
{
    public class BaseMouseEvent : IMouseEvent
    {
        public virtual string GameAction { get; set; }

        public BaseMouseEvent()
        {
        }

        public BaseMouseEvent(string gameAction)
        {
            GameAction = gameAction;
        }

        public virtual bool Evaluate(MouseState previousState, MouseState currentState)
        {
            return false;
        }
    }
}