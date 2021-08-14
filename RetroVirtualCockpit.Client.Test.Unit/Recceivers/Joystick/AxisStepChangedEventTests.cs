using RetroVirtualCockpit.Client.Receivers.Joystick;
using SharpDX.DirectInput;
using Shouldly;
using Xunit;

namespace RetroVirtualCockpit.Client.Test.Unit.Recceivers.Joystick
{
    public class AxisStepChangedEventTests
    {
        public AxisStepChangedEvent _event;

        [Fact]
        public void Evaluate_DoesntReturnsMessage_WhenAxisStateDoesntChange()
        {
            _event = new AxisStepChangedEvent(Axis.Z, 10, "AxisUp", "AxisDown");

            var currentState = new JoystickState { Z = 0 };
            var previousState = new JoystickState { Z = 0 };

            var message = _event.Evaluate(previousState, currentState);

            message.ShouldBeNull();
        }

        [Fact]
        public void Evaluate_DoesntReturnsMessage_WhenAxisStateChangesWithinAStep()
        {
            _event = new AxisStepChangedEvent(Axis.Z, 10, "AxisUp", "AxisDown");

            var currentState = new JoystickState { Z = (ushort.MaxValue / 10) - 1 };
            var previousState = new JoystickState { Z = 0 };

            var message = _event.Evaluate(previousState, currentState);

            message.ShouldBeNull();
        }

        [Fact]
        public void Evaluate_ReturnsUpMessage_WhenAxisChangesAboveAStep()
        {
            _event = new AxisStepChangedEvent(Axis.Z, 10, "AxisUp", "AxisDown");

            var currentState = new JoystickState { Z = (ushort.MaxValue / 10) + 1 };
            var previousState = new JoystickState { Z = 0 };

            var message = _event.Evaluate(previousState, currentState);

            message.ShouldNotBeNull();
            message.MessageText.ShouldBe("AxisUp");
        }

        [Fact]
        public void Evaluate_ReturnsSingleUpMessage_WhenAxisChangesToMaxValue()
        {
            _event = new AxisStepChangedEvent(Axis.Z, 10, "AxisUp", "AxisDown");

            var currentState = new JoystickState { Z = ushort.MaxValue };
            var previousState = new JoystickState { Z = 0 };

            var message = _event.Evaluate(previousState, currentState);

            message.ShouldNotBeNull();
            message.MessageText.ShouldBe("AxisUp");
        }

        [Fact]
        public void Evaluate_ReturnsDownMessage_WhenAxisChangesBelowAStep()
        {
            _event = new AxisStepChangedEvent(Axis.Z, 10, "AxisUp", "AxisDown");

            var currentState = new JoystickState { Z = 0 };
            var previousState = new JoystickState { Z = (ushort.MaxValue / 10) + 1 };

            var message = _event.Evaluate(previousState, currentState);

            message.ShouldNotBeNull();
            message.MessageText.ShouldBe("AxisDown");
        }
    }
}
