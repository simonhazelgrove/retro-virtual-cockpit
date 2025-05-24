using Xunit;
using System.Collections.Generic;
using RetroVirtualCockpit.Server.Receivers;
using RetroVirtualCockpit.Server.Receivers.Mouse;
using SharpDX.DirectInput;
using Shouldly;

namespace RetroVirtualCockpit.Server.Test.Unit.Recceivers
{
    public class MouseReceiverTests
    {
        // Test mouse event class
        public class TestMouseEvent : BaseMouseEvent, IMouseEvent
        {
            public override bool Evaluate(MouseState previousState, MouseState currentState)
            {
                return true;
            }
        }

        private MouseReceiver _mouseReceiver;

        public MouseReceiverTests()
        {
            _mouseReceiver = new MouseReceiver(null);
        }

        [Fact]
        public void ReceiveInputs_EvaluatesAllEvents()
        {
            _mouseReceiver.SetEvents(GetTestEvents());

            _mouseReceiver.CurrentState = new MouseState();
            _mouseReceiver.PreviousState = new MouseState();

            _mouseReceiver.ReceiveInput();

            var messages = _mouseReceiver.CollectMessages();

            messages.Count.ShouldBe(3);
            messages[0].ShouldBe("TestMessage1");
            messages[1].ShouldBe("TestMessage2");
            messages[2].ShouldBe("TestMessage3");
        }

        private List<IMouseEvent> GetTestEvents()
        {
            return new List<IMouseEvent>
            {
                new TestMouseEvent{ GameAction = "TestMessage1" },
                new TestMouseEvent{ GameAction = "TestMessage2" },
                new TestMouseEvent{ GameAction = "TestMessage3" },
            };
        }
    }
}