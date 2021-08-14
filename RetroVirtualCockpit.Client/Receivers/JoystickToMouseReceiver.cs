using System.Drawing;
using RetroVirtualCockpit.Client.Helpers;

namespace RetroVirtualCockpit.Client.Receivers
{
    /// <summary>
    /// Not yet ready for use
    /// </summary>
    public class JoystickToMouseReceiver : JoystickReceiver
    {
        private Point _mousePos;

        private float _mouseMoveX;

        private float _mouseMoveY;

        private bool _resetMouse;

        public JoystickToMouseReceiver(SharpDX.DirectInput.Joystick joystick) : base(joystick)
        {
            Mouse.GetCursorPos(ref _mousePos);
            _mousePos.X = _mousePos.X * 34;
            _mousePos.Y = _mousePos.Y * 61;
        }

        public override void ReceiveInput()
        {
            base.ReceiveInput();

            if (CurrentState != null)
            {
                //_mouseMoveX = _currentState.X;
                //_mouseMoveY = _currentState.Y;

                // Joystick movement
                //_inputSimulator.Mouse.MoveMouseTo(_mouseX + _mouseMoveX / 10f, _mouseY + _mouseMoveY / 10f);

                // If you use this line, mouse just continues moving in whatever direction, you dont need the _resetMouse code
                ////_inputSimulator.Mouse.MoveMouseBy((int)(_mouseMoveX / 30000f), (int)(_mouseMoveY / 30000f));
                _resetMouse = true;
            }
            else
            {
                if (_resetMouse)
                {
                    //_inputSimulator.Mouse.MoveMouseTo(_mouseX - _mouseMoveX * 12, _mouseY - _mouseMoveY * 12);
                    _resetMouse = false;
                }

                // No joystick movement - save current mouse position
                Mouse.GetCursorPos(ref _mousePos);
                _mousePos.X = _mousePos.X * 34;
                _mousePos.Y = _mousePos.Y * 61;
            }
        }
    }
}
