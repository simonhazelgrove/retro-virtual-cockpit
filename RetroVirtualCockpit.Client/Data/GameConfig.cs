using RetroVirtualCockpit.Client.Receivers.Joystick;
using System.Collections.Generic;

namespace RetroVirtualCockpit.Client.Data
{
    public class GameConfig
    {
        public string Title { get; set; }

        public Dictionary<string, KeyMapping> KeyMappings { get; set; }

        public List<IJoystickEvent> JoystickMappings { get; set; }
    }
}
