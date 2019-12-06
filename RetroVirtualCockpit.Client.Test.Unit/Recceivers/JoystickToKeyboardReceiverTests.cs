using System;
using NUnit.Framework;
using RetroVirtualCockpit.Client.Messages;
using RetroVirtualCockpit.Client.Receivers;
using SharpDX.DirectInput;
using Shouldly;

namespace RetroVirtualCockpit.Client.Test.Unit.Recceivers
{
    [TestFixture]
    public class JoystickToKeyboardReceiverTests
    {
        private JoystickToKeyboardReceiver _joystickReceiver;

        [SetUp]
        public void SetUp()
        {
            _joystickReceiver = new JoystickToKeyboardReceiver(null);
        }

        [TestCase(0, 0.5, "Controls.Stick.Left")]
        [TestCase(1, 0.5, "Controls.Stick.Right")]
        [TestCase(0.5, 1, "Controls.Stick.Back")]
        [TestCase(0.5, 0, "Controls.Stick.Forward")]
        public void ReceiveInput_ShouldKeyDownWhenJoystickMoved_AndKeyUpWhenJoystickCenteredAgain(double x, double y, string messageText)
        {
            // Start position in center
            SetupJoystickState(0.5, 0.5);
            _joystickReceiver.ReceiveInput();
            var messages = _joystickReceiver.CollectMessages();

            messages.ShouldBeEmpty();

            // Move one axis
            SetupJoystickState(x, y);
            _joystickReceiver.ReceiveInput();
            messages = _joystickReceiver.CollectMessages();

            messages.Count.ShouldBe(1);
            messages[0].ShouldBeOfType<KeyboardMessage>();
            (messages[0] as KeyboardMessage).MessageText.ShouldBe(messageText);
            (messages[0] as KeyboardMessage).Direction.ShouldBe(KeyDirection.Down);

            // Move back to center
            SetupJoystickState(0.5, 0.5);
            _joystickReceiver.ReceiveInput();
            messages = _joystickReceiver.CollectMessages();

            messages.Count.ShouldBe(1);
            messages[0].ShouldBeOfType<KeyboardMessage>();
            (messages[0] as KeyboardMessage).MessageText.ShouldBe(messageText);
            (messages[0] as KeyboardMessage).Direction.ShouldBe(KeyDirection.Up);
        }

        [TestCase(JoystickReceiver.DeadZoneStart, 0.5, JoystickReceiver.DeadZoneStart - 1, 0.5)]
        [TestCase(JoystickReceiver.DeadZoneEnd, 0.5, JoystickReceiver.DeadZoneEnd + 1, 0.5)]
        [TestCase(0.5, JoystickReceiver.DeadZoneStart, 0.5, JoystickReceiver.DeadZoneStart - 1)]
        [TestCase(0.5, JoystickReceiver.DeadZoneEnd, 0.5, JoystickReceiver.DeadZoneEnd + 1)]
        public void ReceiveInput_ShouldNotKeyDown_WhenJoystickMovesWithinDeadzone(double x1, double y1, double x2, double y2)
        {
            // Start position in center
            SetupJoystickState(0.5, 0.5);
            _joystickReceiver.ReceiveInput();
            var messages = _joystickReceiver.CollectMessages();

            messages.ShouldBeEmpty();

            // Move to x1, y1 (within deadzone)
            SetupJoystickState(x1, y1);
            _joystickReceiver.ReceiveInput();
            messages = _joystickReceiver.CollectMessages();

            messages.Count.ShouldBe(0);

            // Move to x2, y2 (outside deadzone)
            SetupJoystickState(x2, y2);
            _joystickReceiver.ReceiveInput();
            messages = _joystickReceiver.CollectMessages();

            messages.Count.ShouldBe(1);
            messages[0].ShouldBeOfType<KeyboardMessage>();
            (messages[0] as KeyboardMessage).Direction.ShouldBe(KeyDirection.Down);
        }

        [TestCase(-1, 0.5, 1, 0.5)]
        [TestCase(1, 0.5, -1, 0.5)]
        [TestCase(0.5, -1, 0.5, 1)]
        [TestCase(0.5, 1, 0.5, -1)]
        public void ReceiveInput_ShouldKeyUp_WhenJoystickMovesInAnotherDirection(double x1, double y1, double x2, double y2)
        {
            // Start position in center
            SetupJoystickState(0.5, 0.5);
            _joystickReceiver.ReceiveInput();
            var messages = _joystickReceiver.CollectMessages();

            messages.ShouldBeEmpty();

            // Move to x1, y1
            SetupJoystickState(x1, y1);
            _joystickReceiver.ReceiveInput();
            messages = _joystickReceiver.CollectMessages();

            messages.Count.ShouldBe(1);
            messages[0].ShouldBeOfType<KeyboardMessage>();
            (messages[0] as KeyboardMessage).Direction.ShouldBe(KeyDirection.Down);

            // Move to x2, y2 (opposite direction)
            SetupJoystickState(x2, y2);
            _joystickReceiver.ReceiveInput();
            messages = _joystickReceiver.CollectMessages();

            messages.Count.ShouldBe(2);
            messages[0].ShouldBeOfType<KeyboardMessage>();
            (messages[0] as KeyboardMessage).Direction.ShouldBe(KeyDirection.Up);
            messages[1].ShouldBeOfType<KeyboardMessage>();
            (messages[1] as KeyboardMessage).Direction.ShouldBe(KeyDirection.Down);
        }

        private void SetupJoystickState(double x, double y)
        {
            _joystickReceiver.PreviousState = _joystickReceiver.CurrentState;

            _joystickReceiver.CurrentState = new JoystickState
            {
                X = GetAbsoluteOrPercentageAxisValue(x),
                Y = GetAbsoluteOrPercentageAxisValue(y)
            };
        }

        private int GetAbsoluteOrPercentageAxisValue(double v)
        {
            if (Math.Abs(v) > 1)
            {
                return (int)v;
            }

            return (int)(v * ushort.MaxValue);
        }
    }
}
