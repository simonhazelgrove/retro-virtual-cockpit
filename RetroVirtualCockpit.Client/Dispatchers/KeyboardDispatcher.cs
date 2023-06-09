using RetroVirtualCockpit.Client.Data;
using RetroVirtualCockpit.Client.Messages;
using System;
using WindowsInput;
using WindowsInput.Native;

namespace RetroVirtualCockpit.Client.Dispatchers
{
    public class KeyboardDispatcher : IDispatch<KeyboardMessage>
    {
        private readonly InputSimulator _inputSimulator;

        public GameConfig SelectedGameConfig;

        public KeyboardDispatcher(InputSimulator inputSimulator)
        {
            _inputSimulator = inputSimulator;
        }

        public void Dispatch(KeyboardMessage message)
        {
            Console.Write(message.MessageText + ": ");

            var modifier = VirtualKeyCode.NONAME;
            var key = VirtualKeyCode.NONAME;
            var keyDirection = message.Direction;
            var keyDownOnly = false;

            if (SelectedGameConfig.KeyMappings.TryGetValue(message.MessageText, out var keyMapping))
            {
                key = keyMapping.KeyCode;

                if (keyMapping.ModifierKeyCode.HasValue)
                {
                    modifier = keyMapping.ModifierKeyCode.Value;
                }

                if (keyMapping.KeyAction == KeyAction.Up)
                {
                    keyDirection = KeyDirection.Up;
                }

                if (keyMapping.KeyAction == KeyAction.Down)
                {
                    keyDownOnly = true;
                }
            }

            if (keyDirection == KeyDirection.Up)
            {
                KeyUp(modifier, key);
            }
            else
            {
                KeyDown(modifier, key);

                if (!keyDownOnly && message.DelayUntilKeyUp.HasValue)
                {
                    SetupKeyUpTimer(message, modifier, key);
                }
            }
        }

        private void SetupKeyUpTimer(KeyboardMessage message, VirtualKeyCode modifier, VirtualKeyCode key)
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

        private void KeyDown(VirtualKeyCode modifier, VirtualKeyCode key)
        {
            if (key != VirtualKeyCode.NONAME)
            {
                if (modifier != VirtualKeyCode.NONAME)
                {
                    _inputSimulator.Keyboard.KeyDown(modifier);
                    Console.WriteLine($"{modifier} down");
                }

                _inputSimulator.Keyboard.KeyDown(key);
                Console.WriteLine($"{key} down");
            }
            else
            {
                Console.WriteLine("*** UNKNOWN ***");
            }
        }

        private void KeyUp(VirtualKeyCode modifier, VirtualKeyCode key)
        {
            if (key != VirtualKeyCode.NONAME)
            {
                _inputSimulator.Keyboard.KeyUp(key);
                Console.WriteLine($"{key} up");

                if (modifier != VirtualKeyCode.NONAME)
                {
                    _inputSimulator.Keyboard.KeyUp(modifier);
                    Console.WriteLine($"{modifier} up");
                }
            }
        }
    }
}
