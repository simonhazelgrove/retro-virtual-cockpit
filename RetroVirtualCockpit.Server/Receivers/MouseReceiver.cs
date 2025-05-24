using System.Collections.Generic;
using RetroVirtualCockpit.Server.Receivers.Mouse;
using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Server.Receivers
{
    public class MouseReceiver : IInputReceiver
    {

        private readonly SharpDX.DirectInput.Mouse _mouse;

        public MouseState CurrentState;

        public MouseState PreviousState;

        protected readonly List<string> _messages;

        protected readonly object _lock;

        private List<IMouseEvent> _events;

        public MouseReceiver(SharpDX.DirectInput.Mouse mouse)
        {
            _messages = new List<string>();
            _lock = new object();
            _mouse = mouse;
        }

        public void SetEvents(List<IMouseEvent> events)
        {
            _events = events;
        }

        public List<string> CollectMessages()
        {
            lock (_lock)
            {
                var messages = new List<string>(_messages);
                _messages.Clear();
                return messages;
            }
        }

        public void ReceiveInput()
        {
            ReadMouseState();

            if (CurrentState != null && PreviousState != null && _events != null)
            {
                foreach (var mouseEvent in _events)
                {
                    if (mouseEvent.Evaluate(PreviousState, CurrentState))
                    {
                        _messages.Add(mouseEvent.GameAction);
                    }
                }
            }
        }

        protected void ReadMouseState()
        {
            if (_mouse != null)
            {
                _mouse.Acquire();
                PreviousState = CurrentState;
                CurrentState = _mouse.GetCurrentState();
            }
        }
    }
}