using RetroVirtualCockpit.Server.Receivers.Joystick;
using RetroVirtualCockpit.Server.Receivers.Mouse;
using System.Collections.Generic;

namespace RetroVirtualCockpit.Server.Data
{
    public class GameConfig
    {
        public string Title { get; set; }

        public Dictionary<string, GameActionMapping> GameActionMappings { get; set; }

        public List<IJoystickEvent> JoystickMappings { get; set; }
        
        public List<IMouseEvent> MouseMappings { get; set; }
    }
}
