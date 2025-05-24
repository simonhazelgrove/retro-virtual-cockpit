using RetroVirtualCockpit.Server.Receivers.Joystick;
using SharpDX.DirectInput;
using Shouldly;
using Xunit;

namespace RetroVirtualCockpit.Server.Test.Unit.Recceivers.Joystick
{
    public class AxisValueChangedEventTests
    {
        private AxisValueChangedEvent _event;

        [Fact]
        public void GameAction_ShouldReturnAction_WhenValueChanges()
        {
            _event = new AxisValueChangedEvent(Axis.Z, 10, "ValueTest");

            // Axis doesnt change
            var currentState = new JoystickState { Z = 0 };
            var previousState = new JoystickState { Z = 0 };

            var changed = _event.Evaluate(previousState, currentState);

            changed.ShouldBeFalse();

            // Axis changes to expected value
            currentState.Z = 10;
            previousState.Z = 1;

            changed = _event.Evaluate(previousState, currentState);
            var action = _event.GameAction;

            changed.ShouldBeTrue();
            action.ShouldBe("ValueTest");
        }
    }
}
