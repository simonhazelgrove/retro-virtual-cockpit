using RetroVirtualCockpit.Server.Receivers.Mouse;
using SharpDX.DirectInput;
using Shouldly;
using Xunit;

namespace RetroVirtualCockpit.Server.Test.Unit.Recceivers.Mouse
{
    public class MouseWheelEventTests
    {
        private MouseWheelEvent _event;
        
        [Fact]
        public void GameAction_ShouldReturnUpAction_WhenValueIncreases()
        {
            _event = new MouseWheelEvent("UpAction", "DownAction");

            // Axis doesnt change
            var currentState = new MouseState { Z = 0 };
            var previousState = new MouseState { Z = 0 };

            var changed = _event.Evaluate(previousState, currentState);

            changed.ShouldBeFalse();

            // Axis changes to expected value
            currentState.Z = 10;
            previousState.Z = 0;

            changed = _event.Evaluate(previousState, currentState);
            var action = _event.GameAction;

            changed.ShouldBeTrue();
            action.ShouldBe("UpAction");
        }
        
        [Fact]
        public void GameAction_ShouldReturnDownAction_WhenValueDecreases()
        {
            _event = new MouseWheelEvent("UpAction", "DownAction");

            // Axis doesnt change
            var currentState = new MouseState { Z = 0 };
            var previousState = new MouseState { Z = 0 };

            var changed = _event.Evaluate(previousState, currentState);

            changed.ShouldBeFalse();

            // Axis changes to expected value
            currentState.Z = -10;
            previousState.Z = 0;

            changed = _event.Evaluate(previousState, currentState);
            var action = _event.GameAction;

            changed.ShouldBeTrue();
            action.ShouldBe("DownAction");
        }
    }
}