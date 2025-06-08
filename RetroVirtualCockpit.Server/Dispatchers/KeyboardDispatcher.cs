using RetroVirtualCockpit.Server.Messages;
using System;
using WindowsInput;
using WindowsInput.Native;

namespace RetroVirtualCockpit.Server.Dispatchers
{
    public class KeyboardDispatcher : IKeyboardDispatcher
    {
        const int DefaultDelay = 100;

        private readonly IKeyboardSimulator _keyboardSimulator;

        public KeyboardDispatcher(InputSimulator inputSimulator)
        {
            _keyboardSimulator = inputSimulator.Keyboard;
        }

        public void Dispatch(KeyboardMessage message)
        {
            ApplyDefaultAction(message);

            if (message.Action == ButtonAction.Up)
            {
                KeyUp(message.Key);

                if (message.Modifier != null)
                {
                    KeyUp(message.Modifier.Value);
                }
            }
            else if (message.Action == ButtonAction.Down)
            {
                if (message.Modifier != null)
                {
                    KeyDown(message.Modifier.Value);
                    SetupTimer(DefaultDelay, () => { KeyDown(message.Key); });

                    if (message.AutoKeyUpDelay.HasValue)
                    {
                        SetupTimer(message.AutoKeyUpDelay.Value * 2, () => { KeyUp(message.Key); });
                        SetupTimer(message.AutoKeyUpDelay.Value * 3, () => { KeyUp(message.Modifier.Value); });
                    }
                }
                else
                {
                    KeyDown(message.Key);

                    if (message.AutoKeyUpDelay.HasValue)
                    {
                        SetupTimer(message.AutoKeyUpDelay ?? DefaultDelay, () => { KeyUp(message.Key); });
                    }
                }
            }
            else if (message.Action == ButtonAction.Press)
            {
                KeyPress(message.Modifier, message.Key);
            }
        }

        private void ApplyDefaultAction(KeyboardMessage message)
        {
            if (message.Action == ButtonAction.None)
            {
                if (message.Modifier != null)
                {
                    message.Action = ButtonAction.Down;
                    message.AutoKeyUpDelay = 100; 
                }
                else
                {
                    message.Action = ButtonAction.Press;
                }
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

        private void KeyPress(KeyCode? modifier, KeyCode key)
        {
            var virtualKeyCode = KeyCodeConverter.ToVirtualKeyCode(key);
            var modifierVirtualKeyCode = KeyCodeConverter.ToVirtualKeyCode(modifier);

            if (virtualKeyCode != VirtualKeyCode.NONAME)
            {
                if (modifierVirtualKeyCode != VirtualKeyCode.NONAME)
                {
                    _keyboardSimulator.ModifiedKeyStroke(modifierVirtualKeyCode, virtualKeyCode);
                    Console.WriteLine($"{modifier} {key} press");
                }
                else
                {
                    _keyboardSimulator.KeyPress(virtualKeyCode);
                    Console.WriteLine($"{key} press");
                }
            }
            else
            {
                Console.WriteLine("*** UNKNOWN ***");
            }
        }

        private void KeyDown(KeyCode key)
        {
            var virtualKeyCode = KeyCodeConverter.ToVirtualKeyCode(key);

            if (virtualKeyCode != VirtualKeyCode.NONAME)
            {
                _keyboardSimulator.KeyDown(virtualKeyCode);
                Console.WriteLine($"{key} down");
            }
            else
            {
                Console.WriteLine("*** UNKNOWN ***");
            }
        }

        private void KeyUp(KeyCode key)
        {
            var virtualKeyCode = KeyCodeConverter.ToVirtualKeyCode(key);

            if (virtualKeyCode != VirtualKeyCode.NONAME)
            {
                _keyboardSimulator.KeyUp(virtualKeyCode);
                Console.WriteLine($"{key} up");
            }
        }
    }
}
