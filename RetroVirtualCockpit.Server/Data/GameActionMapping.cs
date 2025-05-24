using System.Collections.Generic;
using RetroVirtualCockpit.Server.Messages;

namespace RetroVirtualCockpit.Server.Data
{
    public class GameActionMapping : List<Message>
    {
        public GameActionMapping()
        {
        }

        public GameActionMapping(Message message)
        {
            Add(message);
        }

        public GameActionMapping(List<Message> messages)
        {
            AddRange(messages);
        }
       
    }
}
