using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RetroVirtualCockpit.Server.Messages
{
    public class MouseMessage : Message
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public MouseButton Button { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ButtonAction? Action { get; set; }

        public int? DelayUntilButtonUp { get; set; }

        public int MoveX { get; set; }

        public int MoveY { get; set; }

        public MouseMessage()
        {
            Action = ButtonAction.Press;
        }

        public MouseMessage(MouseButton button)
        {
            Button = button;
            Action = ButtonAction.Press;
        }
    }
}
