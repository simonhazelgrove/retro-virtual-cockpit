using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RetroVirtualCockpit.Server.Messages
{
    public class KeyboardMessage : Message
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("key")]
        public KeyCode Key { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("modifier", NullValueHandling = NullValueHandling.Ignore)]
        public KeyCode? Modifier { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("action", NullValueHandling = NullValueHandling.Ignore)]
        public ButtonAction Action { get; set; }

        [JsonProperty("autoKeyUpDelay")]
        public int? AutoKeyUpDelay { get; set; }

        public KeyboardMessage()
        {
        }

        public KeyboardMessage(KeyCode keyCode)
        {
            Key = keyCode;
        }
    }
}
