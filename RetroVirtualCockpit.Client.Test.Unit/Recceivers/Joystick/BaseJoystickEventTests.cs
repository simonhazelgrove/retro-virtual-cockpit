using NUnit.Framework;
using RetroVirtualCockpit.Client.Messages;
using RetroVirtualCockpit.Client.Receivers.Joystick;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroVirtualCockpit.Client.Test.Unit.Recceivers.Joystick
{
    [TestFixture]
    public class BaseJoystickEventTests
    {
        // This class will be used to test as BaseJoystickEvent is abstract
        public class TestJoystickEvent : BaseJoystickEvent
        {
        }

        public class TestMessage : Message
        {
        }

        [Test]
        public void GetMessage_ReturnsMessageTextInMessageObject()
        {
            var testEvent = new TestJoystickEvent { MessageText = "MessageText" };

            var message = testEvent.GetMessage();

            message.ShouldNotBeNull();
            message.ShouldBeOfType<KeyboardMessage>();
            message.MessageText.ShouldBe("MessageText");
        }

        [Test]
        public void GetMessage_ReturnsMessageObject()
        {
            var testEvent = new TestJoystickEvent { Message = new TestMessage { MessageText = "MessageObject" } };

            var message = testEvent.GetMessage();

            message.ShouldNotBeNull();
            message.ShouldBeOfType<TestMessage>();
            message.MessageText.ShouldBe("MessageObject");
        }
    }
}
