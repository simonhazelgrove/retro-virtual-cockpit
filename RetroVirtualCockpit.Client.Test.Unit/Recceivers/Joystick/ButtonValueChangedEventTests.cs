using RetroVirtualCockpit.Client.Messages;
using RetroVirtualCockpit.Client.Receivers.Joystick;
using SharpDX.DirectInput;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace RetroVirtualCockpit.Client.Test.Unit.Recceivers.Joystick
{
    public class ButtonValueChangedEventTests
    {
        public ButtonValueChangedEvent _event;

        [Fact]
        public void Evaluate_ReturnsNothing_WhenButtonStateDoesntChange()
        {
            var buttonIndex = 0;
            var buttonEvent = new ButtonValueChangedEvent(buttonIndex, "ChangeTest");

            // Test when buttons are on
            var currentState = new JoystickState().SetButton(buttonIndex, true);
            var previousState = new JoystickState().SetButton(buttonIndex, true);

            var message = buttonEvent.Evaluate(previousState, currentState);

            message.ShouldBeNull();

            // Test when buttons are off
            currentState.SetButton(buttonIndex, false);
            previousState.SetButton(buttonIndex, false);

            message = buttonEvent.Evaluate(previousState, currentState);

            message.ShouldBeNull();
        }

        [Fact]
        public void Evaluate_ReturnsMessage_WhenButtonStateChanges()
        {
            var buttonIndex = 1;
            var buttonEvent = new ButtonValueChangedEvent(buttonIndex, "ChangeTest");

            // Test when buttons are on
            var currentState = new JoystickState().SetButton(buttonIndex, true);
            var previousState = new JoystickState().SetButton(buttonIndex, false);

            var message = buttonEvent.Evaluate(previousState, currentState);

            message.ShouldNotBeNull();
            message.MessageText.ShouldBe("ChangeTest");

            // Test when buttons are off
            currentState.SetButton(buttonIndex, false);
            previousState.SetButton(buttonIndex, true);

            message = buttonEvent.Evaluate(previousState, currentState);

            message.ShouldNotBeNull();
            message.MessageText.ShouldBe("ChangeTest");
        }

        [Fact]
        public void Evaluate_ReturnsMessage_WhenButtonStateChangesToValue()
        {
            var buttonIndex = 2;
            var buttonEvent = new ButtonValueChangedEvent(buttonIndex, true, "ValueTest");

            // Test when button switches on
            var currentState = new JoystickState().SetButton(buttonIndex, true);
            var previousState = new JoystickState().SetButton(buttonIndex, false);

            var message = buttonEvent.Evaluate(previousState, currentState);

            message.ShouldNotBeNull();
            message.MessageText.ShouldBe("ValueTest");

            // Test when buttons switches off
            currentState.SetButton(buttonIndex, false);
            previousState.SetButton(buttonIndex, true);

            message = buttonEvent.Evaluate(previousState, currentState);

            message.ShouldBeNull();
        }

        [Fact]
        public void Evaluate_ReturnsMessagesFromList_WhenButtonStateChangesToValue()
        {
            var buttonIndex = 3;
            var messageList = new List<string> { "message1", "message2" };
            var buttonEvent = new ButtonValueChangedEvent(buttonIndex, true, messageList);

            // Test when button switches on - should be 1st message
            var currentState = new JoystickState().SetButton(buttonIndex, true);
            var previousState = new JoystickState().SetButton(buttonIndex, false);

            var message = buttonEvent.Evaluate(previousState, currentState);

            message.ShouldNotBeNull();
            message.MessageText.ShouldBe("message1");

            // Test when button switches on - should be 2nd message
            message = buttonEvent.Evaluate(previousState, currentState);

            message.ShouldNotBeNull();
            message.MessageText.ShouldBe("message2");

            // Test when button switches on - should be 1st message again
            message = buttonEvent.Evaluate(previousState, currentState);

            message.ShouldNotBeNull();
            message.MessageText.ShouldBe("message1");
        }

        [Fact]
        public void Evaluate_ReturnsMessageObject_WhenButtonStateChangesToValue()
        {
            var buttonIndex = 4;
            var messageObject = new KeyboardMessage { MessageText = "ObjectTest" };
            var buttonEvent = new ButtonValueChangedEvent(buttonIndex, true, messageObject);

            // Test when button switches on - should return message object
            var currentState = new JoystickState().SetButton(buttonIndex, true);
            var previousState = new JoystickState().SetButton(buttonIndex, false);

            var message = buttonEvent.Evaluate(previousState, currentState);

            message.ShouldNotBeNull();
            message.ShouldBe(messageObject);
        }
    }
}
