using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Server.Receivers.Joystick
{
    public class StickMoveEvent : BaseJoystickAxisEvent, IJoystickEvent
    {
        public string UpGameAction { get; set; }

        public string DownGameAction { get; set; }

        private int pos;

        public override bool Evaluate(JoystickState previousState, JoystickState currentState)
        {
            var currentPos = GetAxisValue(currentState);
            var newPosFloat = currentPos / (float)(ushort.MaxValue);
            var newPosInt = (int)(((newPosFloat * 2) - 1) * 100);

            if (newPosInt != pos)
            {
                pos = newPosInt;
                return true;
            }

            pos = newPosInt;            
            return false;
        }

        public override string GameAction
        {
            get
            {
                if (pos >= 0)
                {
                    return $"{UpGameAction}.{pos}";
                }
                else
                {
                    return $"{DownGameAction}.{pos}";
                }
            }
        }
    }
}