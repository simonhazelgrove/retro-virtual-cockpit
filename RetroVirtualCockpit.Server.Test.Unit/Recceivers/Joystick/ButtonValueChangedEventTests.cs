using RetroVirtualCockpit.Server.Messages;
using RetroVirtualCockpit.Server.Receivers.Joystick;
using SharpDX.DirectInput;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace RetroVirtualCockpit.Server.Test.Unit.Recceivers.Joystick
{
    public class ButtonValueChangedEventTests
    {
        public ButtonValueChangedEvent _event;

        [Fact]
        public void Evaluate_ReturnsFalse_WhenButtonStateDoesntChange()
        {
            var buttonIndex = 0;
            var buttonEvent = new ButtonValueChangedEvent(buttonIndex, "ChangeTest");

            // Test when buttons are on
            var currentState = new JoystickState().SetButton(buttonIndex, true);
            var previousState = new JoystickState().SetButton(buttonIndex, true);

            var changed = buttonEvent.Evaluate(previousState, currentState);

            changed.ShouldBeFalse();

            // Test when buttons are off
            currentState.SetButton(buttonIndex, false);
            previousState.SetButton(buttonIndex, false);

            changed = buttonEvent.Evaluate(previousState, currentState);

            changed.ShouldBeFalse();
        }

        [Fact]
        public void GameAction_ReturnsAction_WhenButtonStateChanges()
        {
            var buttonIndex = 1;
            var buttonEvent = new ButtonValueChangedEvent(buttonIndex, "ChangeTest");

            // Test when buttons are on
            var currentState = new JoystickState().SetButton(buttonIndex, true);
            var previousState = new JoystickState().SetButton(buttonIndex, false);

            var changed = buttonEvent.Evaluate(previousState, currentState);
            var action = buttonEvent.GameAction;

            changed.ShouldBeTrue();
            action.ShouldBe("ChangeTest");

            // Test when buttons are off
            currentState.SetButton(buttonIndex, false);
            previousState.SetButton(buttonIndex, true);

            changed = buttonEvent.Evaluate(previousState, currentState);
            action = buttonEvent.GameAction;

            changed.ShouldBeTrue();
            action.ShouldBe("ChangeTest");
        }

        [Fact]
        public void GameAction_ReturnsAction_WhenButtonStateChangesToValue()
        {
            var buttonIndex = 2;
            var buttonEvent = new ButtonValueChangedEvent(buttonIndex, true, "ValueTest");

            // Test when button switches on
            var currentState = new JoystickState().SetButton(buttonIndex, true);
            var previousState = new JoystickState().SetButton(buttonIndex, false);

            var changed = buttonEvent.Evaluate(previousState, currentState);
            var action = buttonEvent.GameAction;

            changed.ShouldBeTrue();
            action.ShouldBe("ValueTest");

            // Test when buttons switches off
            currentState.SetButton(buttonIndex, false);
            previousState.SetButton(buttonIndex, true);

            changed = buttonEvent.Evaluate(previousState, currentState);
            changed.ShouldBeFalse();
        }

        [Fact]
        public void GameAction_ReturnsActionsFromList_WhenButtonStateChangesToValue()
        {
            var buttonIndex = 3;
            var gameActionList = new List<string> { "action1", "action2" };
            var buttonEvent = new ButtonValueChangedEvent(buttonIndex, true, gameActionList);

            // Test when button switches on - should be 1st message
            var currentState = new JoystickState().SetButton(buttonIndex, true);
            var previousState = new JoystickState().SetButton(buttonIndex, false);

            var changed = buttonEvent.Evaluate(previousState, currentState);
            var action = buttonEvent.GameAction;

            changed.ShouldBeTrue();
            action.ShouldBe("action1");

            // Test when button switches on - should be 2nd action
            changed = buttonEvent.Evaluate(previousState, currentState);
            action = buttonEvent.GameAction;

            changed.ShouldBeTrue();
            action.ShouldBe("action2");

            // Test when button switches on - should be 1st action again
            changed = buttonEvent.Evaluate(previousState, currentState);
            action = buttonEvent.GameAction;

            changed.ShouldBeTrue();
            action.ShouldBe("action1");
        }

        [Fact]
        public void Evaluate_ReturnsTrue_WhenButtonStateChangesToValue()
        {
            var buttonIndex = 4;
            var buttonEvent = new ButtonValueChangedEvent(buttonIndex, true, "Test");

            // Test when button switches on - should return message object
            var currentState = new JoystickState().SetButton(buttonIndex, true);
            var previousState = new JoystickState().SetButton(buttonIndex, false);

            var changed = buttonEvent.Evaluate(previousState, currentState);

            changed.ShouldBeTrue();
        }
    }
}
