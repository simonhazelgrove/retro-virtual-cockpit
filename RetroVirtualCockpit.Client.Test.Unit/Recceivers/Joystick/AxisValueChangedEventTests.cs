using NUnit.Framework;
using RetroVirtualCockpit.Client.Receivers.Joystick;
using SharpDX.DirectInput;
using Shouldly;

namespace RetroVirtualCockpit.Client.Test.Unit.Recceivers.Joystick
{
    [TestFixture]
    public class AxisValueChangedEventTests
    {
        private AxisValueChangedEvent _event;

        [Test]
        public void Evaluate_ShouldReturnMessageWhenValueChanges()
        {
            _event = new AxisValueChangedEvent(Axis.Z, 10, "ValueTest");

            // Axis doesnt change
            var currentState = new JoystickState { Z = 0 };
            var previousState = new JoystickState { Z = 0 };

            var message = _event.Evaluate(previousState, currentState);

            message.ShouldBeNull();

            // Axis changes to expected value
            currentState.Z = 10;
            previousState.Z = 1;

            message = _event.Evaluate(previousState, currentState);

            message.ShouldNotBeNull();
            message.MessageText.ShouldBe("ValueTest");
        }
    }
}
