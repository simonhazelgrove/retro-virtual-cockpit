using RetroVirtualCockpit.Server.Messages;
using System;
using WindowsInput;
using WindowsInput.Native;

namespace RetroVirtualCockpit.Server.Dispatchers
{
    public class KeyboardDispatcher : IDispatch<KeyboardMessage>
    {
        private readonly IKeyboardSimulator _keyboardSimulator;

        public KeyboardDispatcher(IKeyboardSimulator keyboardSimulator)
        {
            _keyboardSimulator = keyboardSimulator;
        }

        public void Dispatch(KeyboardMessage message)
        {
            if (message.Action == ButtonAction.Up)
            {
                KeyUp(message.Modifier, message.Key);
            }
            else if (message.Action == ButtonAction.Down)
            {
                if (message.Modifier != null)
                {
                    KeyDown(null, message.Modifier.Value);
                    SetupTimer(100, () => { KeyDown(null, message.Key); });
                    SetupTimer(200, () => { KeyUp(null, message.Key); });
                    SetupTimer(300, () => { KeyUp(null, message.Modifier.Value); });
                }
                else
                {
                    KeyDown(null, message.Key);

                    if (message.Action == ButtonAction.Down && message.DelayUntilKeyUp.HasValue)
                    {
                        SetupKeyUpTimer(message, message.Modifier, message.Key);
                    }
                }
            }
            else if (message.Action == ButtonAction.Press)
            {
               KeyPress(message.Modifier, message.Key);
            }
        }

        private void SetupTimer(int milliseconds, Action action)
        {
            var timer = new System.Timers.Timer
            {
                Interval = milliseconds
            };
            timer.Elapsed += (o, e) =>
            {
                action();
                timer.Stop();
            };
            timer.Start();
        }

        private void SetupKeyDownTimer(KeyboardMessage message, KeyCode? modifier, KeyCode key)
        {
            var timer = new System.Timers.Timer
            {
                Interval = message.DelayUntilKeyUp.Value
            };
            timer.Elapsed += (o, e) =>
            {
                KeyDown(modifier, key);
                timer.Stop();
            };
            timer.Start();
        }

        private void SetupKeyUpTimer(KeyboardMessage message, KeyCode? modifier, KeyCode key)
        {
            var timer = new System.Timers.Timer
            {
                Interval = message.DelayUntilKeyUp.Value
            };
            timer.Elapsed += (o, e) =>
            {
                KeyUp(modifier, key);
                timer.Stop();
            };
            timer.Start();
        }

        private void KeyPress(KeyCode? modifier, KeyCode key)
        {
            var virtualKeyCode = KeyCodeConverter.ToVirtualKeyCode(key);
            var modifierVirtualKeyCode = KeyCodeConverter.ToVirtualKeyCode(modifier);

            if (virtualKeyCode != VirtualKeyCode.NONAME)
            {
                if (modifierVirtualKeyCode != VirtualKeyCode.NONAME)
                {
                    _keyboardSimulator.KeyPress(modifierVirtualKeyCode);
                    Console.WriteLine($"{modifier} {key} press");
                }
                else
                {
                    Console.WriteLine($"{key} press");
                }

                _keyboardSimulator.KeyPress(virtualKeyCode, modifierVirtualKeyCode);
            }
            else
            {
                Console.WriteLine("*** UNKNOWN ***");
            }
        }

        private void KeyDown(KeyCode? modifier, KeyCode key)
        {
            var virtualKeyCode = KeyCodeConverter.ToVirtualKeyCode(key);
            var modifierVirtualKeyCode = KeyCodeConverter.ToVirtualKeyCode(modifier);

            if (virtualKeyCode != VirtualKeyCode.NONAME)
            {
                if (modifierVirtualKeyCode != VirtualKeyCode.NONAME)
                {
                    _keyboardSimulator.KeyDown(modifierVirtualKeyCode);
                    Console.WriteLine($"{modifier} down");
                }

                _keyboardSimulator.KeyDown(virtualKeyCode);
                Console.WriteLine($"{key} down");
            }
            else
            {
                Console.WriteLine("*** UNKNOWN ***");
            }
        }

        private void KeyUp(KeyCode? modifier, KeyCode key)
        {
            var virtualKeyCode = KeyCodeConverter.ToVirtualKeyCode(key);
            var modifierVirtualKeyCode = KeyCodeConverter.ToVirtualKeyCode(modifier);

            if (virtualKeyCode != VirtualKeyCode.NONAME)
            {
                _keyboardSimulator.KeyUp(virtualKeyCode);
                Console.WriteLine($"{key} up");

                if (modifierVirtualKeyCode != VirtualKeyCode.NONAME)
                {
                    _keyboardSimulator.KeyUp(modifierVirtualKeyCode);
                    Console.WriteLine($"{modifier} up");
                }
            }
        }
    }
}
