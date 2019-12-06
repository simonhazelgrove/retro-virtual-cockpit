using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Client.Test.Unit.Recceivers.Joystick
{
    public static class TestExtensions
    {
        public static JoystickState SetButton(this JoystickState state, int buttonIndex, bool value)
        {
            state.Buttons[buttonIndex] = value;

            return state;
        }

        public static JoystickState SetPov(this JoystickState state, int povIndex, int value)
        {
            state.PointOfViewControllers[povIndex] = value;

            return state;
        }
    }
}
