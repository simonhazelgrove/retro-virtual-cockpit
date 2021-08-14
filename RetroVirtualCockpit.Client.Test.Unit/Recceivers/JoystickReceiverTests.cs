using System;
using Xunit;
using System.Collections.Generic;
using RetroVirtualCockpit.Client.Messages;
using RetroVirtualCockpit.Client.Receivers;
using RetroVirtualCockpit.Client.Receivers.Joystick;
using SharpDX.DirectInput;
using Shouldly;

namespace RetroVirtualCockpit.Client.Test.Unit.Recceivers
{
    public class JoystickReceiverTests
    {
        // This subclass is used in this test as JoystickReceiver is abstract
        private class TestJoystickReceiver : JoystickReceiver
        {
            public TestJoystickReceiver(SharpDX.DirectInput.Joystick joystick) : base(joystick)
            {
            }
        }

        // Test joystick event class
        private class TestJoystickEvent : BaseJoystickEvent, IJoystickEvent
        {
            public Message Evaluate(JoystickState previousState, JoystickState currentState)
            {
                return GetMessage();
            }
        }

        private TestJoystickReceiver _joystickReceiver;

        public JoystickReceiverTests()
        {
            _joystickReceiver = new TestJoystickReceiver(null);
        }

        [Fact]
        public void ReceiveInputs_EvaluatesAllEvents()
        {
            _joystickReceiver.SetEvents(GetTestEvents());

            _joystickReceiver.CurrentState = new JoystickState();
            _joystickReceiver.PreviousState = new JoystickState();

            _joystickReceiver.ReceiveInput();

            var messages = _joystickReceiver.CollectMessages();

            messages.Count.ShouldBe(3);
            messages[0].MessageText.ShouldBe("TestMessage1");
            messages[1].MessageText.ShouldBe("TestMessage2");
            messages[2].MessageText.ShouldBe("TestMessage3");
        }

        private List<IJoystickEvent> GetTestEvents()
        {
            return new List<IJoystickEvent>
            {
                new TestJoystickEvent{ MessageText = "TestMessage1" },
                new TestJoystickEvent{ MessageText = "TestMessage2" },
                new TestJoystickEvent{ MessageText = "TestMessage3" },
            };
        }
    }
}
