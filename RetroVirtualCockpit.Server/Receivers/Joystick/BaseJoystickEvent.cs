using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Server.Receivers.Joystick
{
    public abstract class BaseJoystickEvent : IJoystickEvent
    {
        public virtual string GameAction { get; set; }

        public BaseJoystickEvent()
        {
        }

        public BaseJoystickEvent(string gameAction)
        {
            GameAction = gameAction;
        }

        public virtual bool Evaluate(JoystickState previousState, JoystickState currentState)
        {
            return false;
        }
    }
}
