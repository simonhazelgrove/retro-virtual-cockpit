using NSubstitute;
using Xunit;
using RetroVirtualCockpit.Server.Dispatchers;
using RetroVirtualCockpit.Server.Messages;
using System;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;
using System.Threading;

namespace RetroVirtualCockpit.Server.Test.Unit.Dispatchers
{
    public class KeyboardDispatcherTests
    {
        private KeyboardDispatcher _dispatcher;

        private readonly InputSimulator _inputSimulator;

        private IKeyboardSimulator _mockKeyboardSimulator;

        public KeyboardDispatcherTests()
        {
            _mockKeyboardSimulator = Substitute.For<IKeyboardSimulator>();
            _inputSimulator = new InputSimulator(_mockKeyboardSimulator, null, null);

            _dispatcher = new KeyboardDispatcher(_inputSimulator);
        }

        [Fact]
        public void Dispatch_ShouldPressAKeyDown()
        {
            var message = new KeyboardMessage { Key = KeyCode.Space };

            _dispatcher.Dispatch(message);

            _mockKeyboardSimulator.Received(1).KeyDown(VirtualKeyCode.SPACE);
        }

        [Fact]
        public void Dispatch_ShouldPressAKeyDownWithModifierFirst()
        {
            var message = new KeyboardMessage { Key = KeyCode.Enter, Modifier = KeyCode.Control };

            _dispatcher.Dispatch(message);
            Thread.Sleep(500);

            _mockKeyboardSimulator.Received(2).KeyDown(Arg.Any<VirtualKeyCode>());
            _mockKeyboardSimulator.Received(2).KeyUp(Arg.Any<VirtualKeyCode>());

            Received.InOrder(() => {
                _mockKeyboardSimulator.KeyDown(VirtualKeyCode.CONTROL);
                _mockKeyboardSimulator.KeyDown(VirtualKeyCode.RETURN);
                _mockKeyboardSimulator.KeyUp(VirtualKeyCode.RETURN);
                _mockKeyboardSimulator.KeyUp(VirtualKeyCode.CONTROL);
            });
        }

        [Fact]
        public void Dispatch_ShouldPressAKeyUp()
        {
            var message = new KeyboardMessage { Key = KeyCode.Space, Action = ButtonAction.Up };

            _dispatcher.Dispatch(message);

            _mockKeyboardSimulator.Received(1).KeyUp(VirtualKeyCode.SPACE);
        }

        [Fact]
        public void Dispatch_ShouldPressAKeyUpWithModifierLast()
        {
            var message = new KeyboardMessage { Key = KeyCode.Enter, Modifier = KeyCode.Control, Action = ButtonAction.Up };

            _dispatcher.Dispatch(message);

            _mockKeyboardSimulator.Received(2).KeyUp(Arg.Any<VirtualKeyCode>());

            Received.InOrder(() => {
                _mockKeyboardSimulator.KeyUp(VirtualKeyCode.RETURN);
                _mockKeyboardSimulator.KeyUp(VirtualKeyCode.CONTROL);
            });
        }

        [Fact]
        public void Dispatch_ShouldPressAKeyUpAfterSomeTime()
        {
            var message = new KeyboardMessage { Key = KeyCode.Space, DelayUntilKeyUp = 200 };

            _dispatcher.Dispatch(message);

            Thread.Sleep(300);

            _mockKeyboardSimulator.Received(1).KeyDown(Arg.Any<VirtualKeyCode>());
            _mockKeyboardSimulator.Received(1).KeyUp(Arg.Any<VirtualKeyCode>());

            Received.InOrder(() => {
                _mockKeyboardSimulator.KeyDown(VirtualKeyCode.SPACE);
                _mockKeyboardSimulator.KeyUp(VirtualKeyCode.SPACE);
            });
        }

        [Fact]
        public void Dispatch_ShouldNotPressAKeyUpAfterSomeTime_IfDelayUntilKeyUpIsNotSet()
        {
            var message = new KeyboardMessage { Key = KeyCode.Space };

            _dispatcher.Dispatch(message);

            Thread.Sleep(300);

            _mockKeyboardSimulator.Received(1).KeyDown(Arg.Any<VirtualKeyCode>());
            _mockKeyboardSimulator.Received(0).KeyUp(Arg.Any<VirtualKeyCode>());
        }
    }
}
