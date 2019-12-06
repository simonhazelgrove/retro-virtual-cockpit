using SharpDX.DirectInput;
using System;

namespace RetroVirtualCockpit.Client.Receivers.Joystick
{
    public class BaseJoystickAxisEvent : BaseJoystickEvent
    {
        public Axis Axis { get; set; }

        public BaseJoystickAxisEvent(Axis axis)
        {
            Axis = axis;
        }

        public BaseJoystickAxisEvent(Axis axis, string messageText) : base(messageText)
        {
            Axis = axis;
        }

        protected int GetAxisValue(JoystickState state)
        {
            if (Axis == Axis.X)
            {
                return state.X;
            }
            else if (Axis == Axis.Y)
            {
                return state.Y;
            }
            else if (Axis == Axis.Z)
            {
                return state.Z;
            }

            throw new Exception($"Unknown Axis '{Axis}'");
        }
    }
}
