using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RetroVirtualCockpit.Server.Messages
{
    public class MessageJsonConverter : JsonCreationConverter<Message>
    {
        protected override Message Create(Type objectType, JObject jObject)
        {
            if (FieldExists("key", jObject))
            {
                return new KeyboardMessage();
            }
            else if (FieldExists("Button", jObject))
            {
                return new MouseMessage();
            }
            else
            {
                throw new Exception($"{GetType().Name} cannot identify JSON Message");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}