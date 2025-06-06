﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RetroVirtualCockpit.Server.Messages
{
    public class KeyboardMessage : Message
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode Key { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public KeyCode? Modifier { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ButtonAction? Action { get; set; }

        public int? DelayUntilKeyUp { get; set; }

        public KeyboardMessage()
        {
            Action = ButtonAction.Down;
        }

        public KeyboardMessage(KeyCode keyCode)
        {
            Key = keyCode;
            Action = ButtonAction.Down;
        }
    }
}
