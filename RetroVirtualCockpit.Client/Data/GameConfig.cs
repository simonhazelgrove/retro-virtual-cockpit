using System.Collections.Generic;

namespace RetroVirtualCockpit.Client.Data
{
    public class GameConfig
    {
        public string Title { get; set; }

        public Dictionary<string, KeyMapping> KeyMappings { get; set; }
    }
}
