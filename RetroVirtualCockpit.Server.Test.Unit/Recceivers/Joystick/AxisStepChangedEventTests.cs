using RetroVirtualCockpit.Server.Receivers.Joystick;
using SharpDX.DirectInput;
using Shouldly;
using Xunit;

namespace RetroVirtualCockpit.Server.Test.Unit.Recceivers.Joystick
{
    public class AxisStepChangedEventTests
    {
        public AxisStepChangedEvent _event;

        [Fact]
        public void Evaluate_ReturnsFalse_WhenAxisStateDoesntChange()
        {
            _event = new AxisStepChangedEvent(Axis.Z, 10, "AxisUp", "AxisDown");

            var currentState = new JoystickState { Z = 0 };
            var previousState = new JoystickState { Z = 0 };

            var changed = _event.Evaluate(previousState, currentState);

            changed.ShouldBeFalse();
        }

        [Fact]
        public void Evaluate_ReturnsFalse_WhenAxisStateChangesWithinAStep()
        {
            _event = new AxisStepChangedEvent(Axis.Z, 10, "AxisUp", "AxisDown");

            var currentState = new JoystickState { Z = (ushort.MaxValue / 10) - 1 };
            var previousState = new JoystickState { Z = 0 };

            var changed = _event.Evaluate(previousState, currentState);

            changed.ShouldBeFalse();
        }

        [Fact]
        public void Evaluate_ReturnsUpMessage_WhenAxisChangesAboveAStep()
        {
            _event = new AxisStepChangedEvent(Axis.Z, 10, "AxisUp", "AxisDown");

            var currentState = new JoystickState { Z = (ushort.MaxValue / 10) + 1 };
            var previousState = new JoystickState { Z = 0 };

            var changed = _event.Evaluate(previousState, currentState);
            var action = _event.GameAction;

            changed.ShouldBeTrue();
            action.ShouldBe("AxisUp");
        }

        [Fact]
        public void GameAction_ReturnsAction_WhenAxisChangesToMaxValue()
        {
            _event = new AxisStepChangedEvent(Axis.Z, 10, "AxisUp", "AxisDown");

            var currentState = new JoystickState { Z = ushort.MaxValue };
            var previousState = new JoystickState { Z = 0 };

            var changed = _event.Evaluate(previousState, currentState);
            var action = _event.GameAction;

            changed.ShouldBeTrue();
            action.ShouldBe("AxisUp");
        }

        [Fact]
        public void GameAction_ReturnsDownMessage_WhenAxisChangesBelowAStep()
        {
            _event = new AxisStepChangedEvent(Axis.Z, 10, "AxisUp", "AxisDown");

            var currentState = new JoystickState { Z = 0 };
            var previousState = new JoystickState { Z = (ushort.MaxValue / 10) + 1 };

            var changed = _event.Evaluate(previousState, currentState);
            var action = _event.GameAction;

            changed.ShouldBeTrue();
            action.ShouldBe("AxisDown");
        }
    }
}
