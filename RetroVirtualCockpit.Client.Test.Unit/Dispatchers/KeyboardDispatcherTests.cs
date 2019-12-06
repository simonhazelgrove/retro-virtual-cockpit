using NSubstitute;
using NUnit.Framework;
using RetroVirtualCockpit.Client.Data;
using RetroVirtualCockpit.Client.Dispatchers;
using RetroVirtualCockpit.Client.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace RetroVirtualCockpit.Client.Test.Unit.Dispatchers
{
    [TestFixture]
    public class KeyboardDispatcherTests
    {
        private KeyboardDispatcher _dispatcher;

        private InputSimulator _inputSimulator;

        private IKeyboardSimulator _mockKeyboardSimulator;

        [SetUp]
        public void Setup()
        {
            _mockKeyboardSimulator = Substitute.For<IKeyboardSimulator>();

            _inputSimulator = new InputSimulator(_mockKeyboardSimulator, null, null);

            _dispatcher = new KeyboardDispatcher(_inputSimulator);

            _dispatcher.SelectedGameConfig = new Data.GameConfig
            {
                KeyMappings = new Dictionary<string, KeyMapping>
                {
                    { "space", new KeyMapping { KeyCode = VirtualKeyCode.SPACE } },
                    { "ctrl.return", new KeyMapping { KeyCode = VirtualKeyCode.RETURN, ModifierKeyCode = VirtualKeyCode.CONTROL } }
                }
            };
        }

        [Test]
        public void Dispatch_ShouldPressAKeyDown()
        {
            var message = new KeyboardMessage { MessageText = "space" };

            _dispatcher.Dispatch(message);

            _mockKeyboardSimulator.Received(1).KeyDown(VirtualKeyCode.SPACE);
        }

        [Test]
        public void Dispatch_ShouldPressAKeyDownWithModifierFirst()
        {
            var message = new KeyboardMessage { MessageText = "ctrl.return" };

            _dispatcher.Dispatch(message);

            _mockKeyboardSimulator.Received(2).KeyDown(Arg.Any<VirtualKeyCode>());

            Received.InOrder(() => {
                _mockKeyboardSimulator.KeyDown(VirtualKeyCode.CONTROL);
                _mockKeyboardSimulator.KeyDown(VirtualKeyCode.RETURN);
            });
        }

        [Test]
        public void Dispatch_ShouldPressAKeyUp()
        {
            var message = new KeyboardMessage { MessageText = "space", Direction = KeyDirection.Up };

            _dispatcher.Dispatch(message);

            _mockKeyboardSimulator.Received(1).KeyUp(VirtualKeyCode.SPACE);
        }

        [Test]
        public void Dispatch_ShouldPressAKeyUpWithModifierLast()
        {
            var message = new KeyboardMessage { MessageText = "ctrl.return", Direction = KeyDirection.Up };

            _dispatcher.Dispatch(message);

            _mockKeyboardSimulator.Received(2).KeyUp(Arg.Any<VirtualKeyCode>());

            Received.InOrder(() => {
                _mockKeyboardSimulator.KeyUp(VirtualKeyCode.RETURN);
                _mockKeyboardSimulator.KeyUp(VirtualKeyCode.CONTROL);
            });
        }

        [Test]
        public async Task Dispatch_ShouldPressAKeyUpAfterSomeTime()
        {
            var message = new KeyboardMessage { MessageText = "space", DelayUntilKeyUp = 100 };

            _dispatcher.Dispatch(message);

            await AsyncWaitMilliseconds(200);

            _mockKeyboardSimulator.Received(1).KeyDown(Arg.Any<VirtualKeyCode>());
            _mockKeyboardSimulator.Received(1).KeyUp(Arg.Any<VirtualKeyCode>());

            Received.InOrder(() => {
                _mockKeyboardSimulator.KeyDown(VirtualKeyCode.SPACE);
                _mockKeyboardSimulator.KeyUp(VirtualKeyCode.SPACE);
            });
        }

        [Test]
        public async Task Dispatch_ShouldNotPressAnyKeys_IfKeymappingNotFound()
        {
            var message = new KeyboardMessage { MessageText = "doesnt.exist", DelayUntilKeyUp = 100 };

            _dispatcher.Dispatch(message);

            await AsyncWaitMilliseconds(200);

            _mockKeyboardSimulator.Received(0).KeyDown(Arg.Any<VirtualKeyCode>());
            _mockKeyboardSimulator.Received(0).KeyUp(Arg.Any<VirtualKeyCode>());
        }

        public Task AsyncWaitMilliseconds(int milliseconds)
        {
            var endTime = DateTime.Now.AddMilliseconds(milliseconds);

            return Task.Run(() =>
            {
                while (DateTime.Now < endTime) ;
            });
        }
    }
}
