using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Server.Receivers.Joystick
{
    public interface IJoystickEvent
    {
        public string GameAction { get; set; }
        
        bool Evaluate(JoystickState previousState, JoystickState currentState);
    }
}
