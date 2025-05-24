using RetroVirtualCockpit.Server.Messages;

namespace RetroVirtualCockpit.Server.Test.Unit.Messages
{
    public class TestMessageContainer
    {
        public Message Message { get; set; }
    }

    public class InvalidMessage : Message {};
}