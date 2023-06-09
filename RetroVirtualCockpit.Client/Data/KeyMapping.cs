using WindowsInput.Native;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RetroVirtualCockpit.Client.Data
{
    public class KeyMapping
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public VirtualKeyCode KeyCode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public VirtualKeyCode? ModifierKeyCode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public KeyAction? KeyAction { get; set; }

        public KeyMapping()
        {
        }

        public KeyMapping(VirtualKeyCode keyCode)
        {
            KeyCode = keyCode;
            ModifierKeyCode = null;
            KeyAction = null;
        }

        public KeyMapping(VirtualKeyCode modifierKeyCode, VirtualKeyCode keyCode)
        {
            KeyCode = keyCode;
            ModifierKeyCode = modifierKeyCode;
        }

        public KeyMapping(VirtualKeyCode keyCode, KeyAction keyAction)
        {
            KeyCode = keyCode;
            KeyAction = keyAction;
        }
    }
}
