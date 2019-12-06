using RetroVirtualCockpit.Client.Messages;
using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Client.Receivers.Joystick
{
    public interface IJoystickEvent
    {
        Message Evaluate(JoystickState previousState, JoystickState currentState);
    }
}
