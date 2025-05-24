using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RetroVirtualCockpit.Server.Receivers.Joystick
{
    public class JoystickEventJsonConverter : JsonCreationConverter<IJoystickEvent>
    {
        protected override IJoystickEvent Create(Type objectType, JObject jObject)
        {
            if (FieldExists("ButtonIndex", jObject))
            {
                return new ButtonValueChangedEvent();
            }
            else if (FieldExists("PovIndex", jObject))
            {
                return new PovValueChangedEvent();
            }
            else if (FieldExists("Axis", jObject))
            {
                if (FieldExists("Value", jObject))
                {
                    return new AxisValueChangedEvent();
                }
                else if (FieldExists("Steps", jObject))
                {
                    return new AxisStepChangedEvent();
                }
                else if (FieldExists("UpGameAction", jObject))
                {
                    return new StickMoveEvent();
                }
                else 
                {
                    throw new Exception($"{GetType().Name} cannot identify JSON JoystickAxisEvent");
                }
            }
            else
            {
                throw new Exception($"{GetType().Name} cannot identify JSON JoystickEvent");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}