using RetroVirtualCockpit.Client.Messages;

namespace RetroVirtualCockpit.Client.Receivers.Joystick
{
    public class BaseJoystickEvent
    {
        public string MessageText { get; set; }

        public Message Message { get; set; }

        public BaseJoystickEvent()
        {
        }

        public BaseJoystickEvent(string messageText)
        {
            MessageText = messageText;
        }

        public BaseJoystickEvent(Message message)
        {
            Message = message;
        }

        public virtual Message GetMessage()
        {
            if (Message == null)
            {
                return new KeyboardMessage { MessageText = MessageText };
            }

            return Message;
        }
    }
}
