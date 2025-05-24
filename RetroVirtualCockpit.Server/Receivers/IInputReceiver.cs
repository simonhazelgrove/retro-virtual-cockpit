using System.Collections.Generic;

namespace RetroVirtualCockpit.Server.Receivers
{
    public interface IInputReceiver
    {
        void ReceiveInput();

        List<string> CollectMessages();
    }
}
