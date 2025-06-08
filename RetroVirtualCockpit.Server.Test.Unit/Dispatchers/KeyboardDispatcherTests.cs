using NSubstitute;
using Xunit;
using RetroVirtualCockpit.Server.Dispatchers;
using RetroVirtualCockpit.Server.Messages;
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
        public void Dispatch_ShouldPressAKey()
        {
            // Arrange
            var message = new KeyboardMessage { Key = KeyCode.Space, Action = ButtonAction.Press };

            // Act
            _dispatcher.Dispatch(message);

            // Assert
            _mockKeyboardSimulator.Received(1).KeyPress(VirtualKeyCode.SPACE);
        }

        [Fact]
        public void Dispatch_ShouldPressAKeyWithModifier()
        {
            // Arrange
            var message = new KeyboardMessage { Key = KeyCode.Enter, Action = ButtonAction.Press, Modifier = KeyCode.Control };

            // Act
            _dispatcher.Dispatch(message);
            Thread.Sleep(500);

            // Assert
            _mockKeyboardSimulator.Received(1).ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.RETURN);
            _mockKeyboardSimulator.Received(0).KeyDown(Arg.Any<VirtualKeyCode>());
            _mockKeyboardSimulator.Received(0).KeyUp(Arg.Any<VirtualKeyCode>());
        }

        [Fact]
        public void Dispatch_ShouldPressAKeyDown()
        {
            // Arrange
            var message = new KeyboardMessage { Key = KeyCode.Space, Action = ButtonAction.Down };

            // Act
            _dispatcher.Dispatch(message);

            // Assert
            _mockKeyboardSimulator.Received(1).KeyDown(VirtualKeyCode.SPACE);
        }

        [Fact]
        public void Dispatch_ShouldPressAKeyDownWithModifierFirst()
        {
            // Arrange
            var message = new KeyboardMessage { Key = KeyCode.Enter, Action = ButtonAction.Down, Modifier = KeyCode.Control };

            // Act
            _dispatcher.Dispatch(message);
            Thread.Sleep(300);

            // Assert
            _mockKeyboardSimulator.Received(2).KeyDown(Arg.Any<VirtualKeyCode>());

            Received.InOrder(() =>
            {
                _mockKeyboardSimulator.KeyDown(VirtualKeyCode.CONTROL);
                _mockKeyboardSimulator.KeyDown(VirtualKeyCode.RETURN);
            });
        }

        [Fact]
        public void Dispatch_ShouldPressAKeyBackUpAfterSomeTime_IfAutoKeyUpDelayIsSet()
        {
            // Arrange
            var message = new KeyboardMessage { Key = KeyCode.Space, Action = ButtonAction.Down, AutoKeyUpDelay = 200 };

            // Act
            _dispatcher.Dispatch(message);
            Thread.Sleep(300);

            // Assert
            _mockKeyboardSimulator.Received(1).KeyDown(Arg.Any<VirtualKeyCode>());
            _mockKeyboardSimulator.Received(1).KeyUp(Arg.Any<VirtualKeyCode>());

            Received.InOrder(() =>
            {
                _mockKeyboardSimulator.KeyDown(VirtualKeyCode.SPACE);
                _mockKeyboardSimulator.KeyUp(VirtualKeyCode.SPACE);
            });
        }

        [Fact]
        public void Dispatch_ShouldNotPressAKeyUpAfterSomeTime_IfAutoKeyUpDelayIsNotSet()
        {
            // Arrange
            var message = new KeyboardMessage { Key = KeyCode.Space, Action = ButtonAction.Down };

            // Act
            _dispatcher.Dispatch(message);
            Thread.Sleep(300);

            // Assert
            _mockKeyboardSimulator.Received(1).KeyDown(VirtualKeyCode.SPACE);
            _mockKeyboardSimulator.Received(0).KeyUp(VirtualKeyCode.SPACE);
        }

        [Fact]
        public void Dispatch_ShouldPressAKeyUp()
        {
            // Arrange
            var message = new KeyboardMessage { Key = KeyCode.Space, Action = ButtonAction.Up };

            // Act
            _dispatcher.Dispatch(message);

            // Assert
            _mockKeyboardSimulator.Received(1).KeyUp(VirtualKeyCode.SPACE);
        }

        [Fact]
        public void Dispatch_ShouldPressAKeyUpWithModifierLast()
        {
            // Arrange
            var message = new KeyboardMessage { Key = KeyCode.Enter, Modifier = KeyCode.Control, Action = ButtonAction.Up };

            // Act
            _dispatcher.Dispatch(message);

            // Assert
            _mockKeyboardSimulator.Received(2).KeyUp(Arg.Any<VirtualKeyCode>());

            Received.InOrder(() =>
            {
                _mockKeyboardSimulator.KeyUp(VirtualKeyCode.RETURN);
                _mockKeyboardSimulator.KeyUp(VirtualKeyCode.CONTROL);
            });
        }

        [Fact]
        public void Dispatch_ShouldApplyDefaultActionOfPress_WhenActionIsMissingAndNoModifiers()
        {
            // Arrange
            var message = new KeyboardMessage { Key = KeyCode.Space };

            // Act
            _dispatcher.Dispatch(message);

            // Assert
            Assert.Equal(ButtonAction.Press, message.Action);
            _mockKeyboardSimulator.Received(1).KeyPress(VirtualKeyCode.SPACE);
        }

        [Fact]
        public void Dispatch_ShouldApplyDefaultActionOfKeyDown_WhenActionIsMissingAndModifiersArePresent()
        {
            // Arrange
            var message = new KeyboardMessage { Key = KeyCode.Space, Modifier = KeyCode.Control };

            // Act
            _dispatcher.Dispatch(message);
            Thread.Sleep(300);

            // Assert
            Assert.Equal(ButtonAction.Down, message.Action);
            Assert.Equal(100, message.AutoKeyUpDelay);

            Received.InOrder(() =>
            {
                _mockKeyboardSimulator.KeyDown(VirtualKeyCode.CONTROL);
                _mockKeyboardSimulator.KeyDown(VirtualKeyCode.SPACE);
                _mockKeyboardSimulator.KeyUp(VirtualKeyCode.SPACE);
                _mockKeyboardSimulator.KeyUp(VirtualKeyCode.CONTROL);
            });
        }
    }
}
