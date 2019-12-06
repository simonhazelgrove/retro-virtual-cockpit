using RetroVirtualCockpit.Client.Messages;
using SharpDX.DirectInput;
using System.Collections.Generic;

namespace RetroVirtualCockpit.Client.Receivers.Joystick
{
    public class ButtonValueChangedEvent : BaseJoystickEvent, IJoystickEvent
    {
        public int ButtonIndex { get; set; }

        public bool? Value { get; set; }

        public List<string> Messages;

        private string _lastMessage;

        public ButtonValueChangedEvent(int buttonIndex, string messageText) : base(messageText)
        {
            ButtonIndex = buttonIndex;
        }

        public ButtonValueChangedEvent(int buttonIndex, bool value, string messageText) : base(messageText)
        {
            ButtonIndex = buttonIndex;
            Value = value;
        }

        public ButtonValueChangedEvent(int buttonIndex, bool value, Message message) : base(message)
        {
            ButtonIndex = buttonIndex;
            Value = value;
        }

        public ButtonValueChangedEvent(int buttonIndex, bool value, List<string> messages)
        {
            ButtonIndex = buttonIndex;
            Value = value;
            Messages = messages;
        }

        public Message Evaluate(JoystickState previousState, JoystickState currentState)
        {
            if (!Value.HasValue && currentState.Buttons[ButtonIndex] != previousState.Buttons[ButtonIndex])
            {
                // Button state has changed
                return GetMessage();
            }
            else if (Value.HasValue && currentState.Buttons[ButtonIndex] == Value && previousState.Buttons[ButtonIndex] != Value)
            {
                // Button state has changed to Value
                return GetMessage();
            }

            return null;
        }

        public override Message GetMessage()
        {
            if (Messages != null)
            {
                // Cycle through range of messages
                return NextMessage();
            }

            return base.GetMessage();
        }

        private Message NextMessage()
        {
            var messageIndex = Messages.IndexOf(_lastMessage) + 1;

            if (messageIndex == Messages.Count)
            {
                messageIndex = 0;
            }

            var message = Messages[messageIndex];

            _lastMessage = message;
            return new KeyboardMessage { MessageText = message };
        }
    }
}
