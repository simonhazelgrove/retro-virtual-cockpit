using RetroVirtualCockpit.Server.Receivers.Joystick;

namespace RetroVirtualCockpit.Server.Test.Unit.Recceivers.Joystick
{
    public class TestEventContainer
    {
        public IJoystickEvent JoystickEvent { get; set; }
    }

    public class InvalidJoystickEvent : BaseJoystickEvent, IJoystickEvent {};
}