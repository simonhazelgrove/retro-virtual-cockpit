using System;
using System.Linq;
using Newtonsoft.Json;
using RetroVirtualCockpit.Server.Receivers.Mouse;
using Shouldly;
using Xunit;

namespace RetroVirtualCockpit.Server.Test.Unit.Recceivers.Mouse
{
    public class MouseEventJsonConverterTests
    {
        [Fact]
        public void Create_CreatesAllMouseEvents()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name == "RetroVirtualCockpit.Server");

            var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseMouseEvent))).ToList();

            types.ShouldNotBeEmpty();

            foreach(var t in types)
            {
                // Arrange
                var obj = new TestEventContainer { MouseEvent = (IMouseEvent)Activator.CreateInstance(t) };
                var json = JsonConvert.SerializeObject(obj);

                Should.NotThrow(() =>
                {
                    // Act
                    var obj2 = JsonConvert.DeserializeObject<TestEventContainer>(json, 
                        new JsonConverter[] { 
                            new MouseEventJsonConverter()
                        });                

                    // Assert
                    obj2.MouseEvent.ShouldBeOfType(t);
                }, $"Couldnt create {t.Name} MouseEvent");
            }
        }

        [Fact]
        public void Create_ThrowsExceptionWhenJsonNotRecognised()
        {
            // Arrange
            var obj = new TestEventContainer { MouseEvent = new InvalidMouseEvent() };
            var json = JsonConvert.SerializeObject(obj);

            // Act & assert
            var ex = Should.Throw<Exception>(() =>
            {
                _ = JsonConvert.DeserializeObject<TestEventContainer>(json, 
                    new JsonConverter[] { 
                        new MouseEventJsonConverter()
                    });    
            });
        }
    }
}