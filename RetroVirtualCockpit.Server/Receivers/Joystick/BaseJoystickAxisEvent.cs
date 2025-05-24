using SharpDX.DirectInput;
using System;

namespace RetroVirtualCockpit.Server.Receivers.Joystick
{
    public abstract class BaseJoystickAxisEvent : BaseJoystickEvent
    {
        public Axis Axis { get; set; }

        public BaseJoystickAxisEvent()
        {
        }

        public BaseJoystickAxisEvent(Axis axis)
        {
            Axis = axis;
        }

        public BaseJoystickAxisEvent(Axis axis, string gameAction) : base(gameAction)
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
