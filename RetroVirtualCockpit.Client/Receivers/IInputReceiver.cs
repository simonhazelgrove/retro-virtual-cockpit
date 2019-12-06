using RetroVirtualCockpit.Client.Messages;
using System.Collections.Generic;

namespace RetroVirtualCockpit.Client.Receivers
{
    public interface IInputReceiver
    {
        void ReceiveInput();

        List<Message> CollectMessages();
    }
}
