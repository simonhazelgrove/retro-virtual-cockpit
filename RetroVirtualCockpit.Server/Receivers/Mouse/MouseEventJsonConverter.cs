using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RetroVirtualCockpit.Server.Receivers.Mouse
{
    public class MouseEventJsonConverter : JsonCreationConverter<IMouseEvent>
    {
        protected override IMouseEvent Create(Type objectType, JObject jObject)
        {
            if (FieldExists("UpGameAction", jObject))
            {
                return new MouseWheelEvent();
            }
            else
            {
                throw new Exception($"{GetType().Name} cannot identify JSON MouseEvent");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}