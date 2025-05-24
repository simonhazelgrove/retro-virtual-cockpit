using RetroVirtualCockpit.Server.Receivers.Joystick;
using SharpDX.DirectInput;
using Shouldly;
using Xunit;

namespace RetroVirtualCockpit.Server.Test.Unit.Recceivers.Joystick
{
    public class PovValueChangedEventTests
    {
        public PovValueChangedEvent _event;

        [Fact]
        public void GameAction_ReturnsAction_WhenPovStateChanges()
        {
            var povIndex = 1;
            var povEvent = new PovValueChangedEvent(povIndex, 9000, "ValueTest");

            // Test when pov is resting 
            var currentState = new JoystickState().SetPov(povIndex, -1);
            var previousState = new JoystickState().SetPov(povIndex, 0);

            var changed = povEvent.Evaluate(previousState, currentState);

            changed.ShouldBeFalse();

            // Test when pov changes
            currentState.SetPov(povIndex, 9000);
            previousState.SetPov(povIndex, 0);

            changed = povEvent.Evaluate(previousState, currentState);
            var action = povEvent.GameAction;

            changed.ShouldBeTrue();
            action.ShouldBe("ValueTest");
        }
    }
}
