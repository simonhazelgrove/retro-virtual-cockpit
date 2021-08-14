using RetroVirtualCockpit.Client.Receivers.Joystick;
using SharpDX.DirectInput;
using Shouldly;
using Xunit;

namespace RetroVirtualCockpit.Client.Test.Unit.Recceivers.Joystick
{
    public class PovValueChangedEventTests
    {
        public PovValueChangedEvent _event;

        [Fact]
        public void Evaluate_ReturnsMessage_WhenPovStateChanges()
        {
            var povIndex = 1;
            var povEvent = new PovValueChangedEvent(povIndex, 9000, "ValueTest");

            // Test when pov is resting 
            var currentState = new JoystickState().SetPov(povIndex, -1);
            var previousState = new JoystickState().SetPov(povIndex, 0);

            var message = povEvent.Evaluate(previousState, currentState);

            message.ShouldBeNull();

            // Test when pov changes
            currentState.SetPov(povIndex, 9000);
            previousState.SetPov(povIndex, 0);

            message = povEvent.Evaluate(previousState, currentState);

            message.ShouldNotBeNull();
            message.MessageText.ShouldBe("ValueTest");
        }
    }
}
