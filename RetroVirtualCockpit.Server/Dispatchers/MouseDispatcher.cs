using System;
using System.Drawing;
using RetroVirtualCockpit.Server.Messages;
using RetroVirtualCockpit.Server.Helpers;
using WindowsInput;

namespace RetroVirtualCockpit.Server.Dispatchers
{
    public class MouseDispatcher : IMouseDispatcher
    {
        private IMouseSimulator _mouseSimulator;

        private double _mouseOriginX;

        private double _mouseOriginY;

        private double _mousePosX;

        private double _mousePosY;

        private double _mouseOffsetX;

        private double _mouseOffsetY;

        private bool _isMouseMoved;

        public MouseDispatcher(InputSimulator inputSimulator)
        {
            _mouseSimulator = inputSimulator.Mouse;

            var mouseOrigin = new Point();
            Mouse.GetCursorPos(ref mouseOrigin);

            _mouseOriginX = mouseOrigin.X;
            _mouseOriginY = mouseOrigin.Y;
        }

        public void Dispatch(MouseMessage message)
        {
            if (message.Action == ButtonAction.Press)
            {
                ClickButton(message.Button);
            }
            else if (message.Action == ButtonAction.DoublePress)
            {
                DoubleClickButton(message.Button);
            }
            else if (message.Action == ButtonAction.Down)
            {
                ButtonDown(message.Button);
                
                if (message.DelayUntilButtonUp.HasValue)
                {
                    SetupButtonUpTimer(message.DelayUntilButtonUp.Value, message.Button);
                }
            }
            else if (message.Action == ButtonAction.Up)
            {
                ButtonUp(message.Button);
            }

            // if (_isMouseMoved)
            // {
            //     Move();
            // }
        }

        public void HandleStickMoveX(int amount)
        {
            _mouseOffsetX = amount;
            _isMouseMoved = true;
            Move();
        }

        public void HandleStickMoveY(int amount)
        {
            _mouseOffsetY = amount;
            _isMouseMoved = true;
            Move();
        }

        private void Move()
        {
            if (_mouseOffsetX > -3 && _mouseOffsetX < 3 && _mouseOffsetY > -3 && _mouseOffsetY < 3)
            {
                // Reset mouse origin X
                var mousePos = new Point();
                Mouse.GetCursorPos(ref mousePos);
                
                _mouseOriginX = mousePos.X + 1;
                _mouseOriginY = mousePos.Y + 0.5;
            }

            MoveToAbsolutePosition(_mouseOriginX + _mouseOffsetX, _mouseOriginY + _mouseOffsetY);

            // reset
            _isMouseMoved = false;
        }

        private void MoveToAbsolutePosition(double x, double y)
        {
            var absX = (x / 1920.0) * ushort.MaxValue;
            var absY = (y / 1080.0) * ushort.MaxValue;

            _mouseSimulator.MoveMouseTo(absX, absY);
        }

        private void ClickButton(Messages.MouseButton button)
        {
            if (button == Messages.MouseButton.Left)
            {
                _mouseSimulator.LeftButtonClick();
                Console.WriteLine($"{button} click");
            }
            else if (button == Messages.MouseButton.Right)
            {
                _mouseSimulator.RightButtonClick();
                Console.WriteLine($"{button} click");
            }
        }

        private void DoubleClickButton(Messages.MouseButton button)
        {
            if (button == Messages.MouseButton.Left)
            {
                _mouseSimulator.LeftButtonDoubleClick();
                Console.WriteLine($"{button} double click");
            }
            else if (button == Messages.MouseButton.Right)
            {
                _mouseSimulator.RightButtonDoubleClick();
                Console.WriteLine($"{button} double click");
            }
        }

        private void ButtonDown(Messages.MouseButton button)
        {
            if (button == Messages.MouseButton.Left)
            {
                _mouseSimulator.LeftButtonDown();
                Console.WriteLine($"{button} button down");
            }
            else if (button == Messages.MouseButton.Right)
            {
                _mouseSimulator.RightButtonDown();
                Console.WriteLine($"{button} button down");
            }
        }

        private void ButtonUp(Messages.MouseButton button)
        {
            if (button == Messages.MouseButton.Left)
            {
                _mouseSimulator.LeftButtonUp();
                Console.WriteLine($"{button} button up");
            }
            else if (button == Messages.MouseButton.Right)
            {
                _mouseSimulator.RightButtonUp();
                Console.WriteLine($"{button} button up");
            }
        }

        private void SetupButtonUpTimer(int delayUntilButtonUp, Messages.MouseButton button)
        {
            var timer = new System.Timers.Timer
            {
                Interval = delayUntilButtonUp
            };
            timer.Elapsed += (o, e) =>
            {
                ButtonUp(button);
                timer.Stop();
            };
            timer.Start();
        }
    }
}