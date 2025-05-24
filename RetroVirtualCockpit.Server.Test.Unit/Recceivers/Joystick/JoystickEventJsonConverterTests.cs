using System;
using System.Linq;
using Newtonsoft.Json;
using RetroVirtualCockpit.Server.Receivers.Joystick;
using Shouldly;
using Xunit;

namespace RetroVirtualCockpit.Server.Test.Unit.Recceivers.Joystick
{
    public class JoystickEventJsonConverterTests
    {
        [Fact]
        public void Create_CreatesAllJoystickEvents()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name == "RetroVirtualCockpit.Server");

            var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseJoystickEvent))).ToList();

            types.ShouldNotBeEmpty();

            foreach(var t in types)
            {
                // Arrange
                var obj = new TestEventContainer { JoystickEvent = (IJoystickEvent)Activator.CreateInstance(t) };
                var json = JsonConvert.SerializeObject(obj);

                Should.NotThrow(() =>
                {
                    // Act
                    var obj2 = JsonConvert.DeserializeObject<TestEventContainer>(json, 
                        new JsonConverter[] { 
                            new JoystickEventJsonConverter()
                        });                

                    // Assert
                    obj2.JoystickEvent.ShouldBeOfType(t);
                }, $"Couldnt create {t.Name} JoystickEvent");
            }
        }

        [Fact]
        public void Create_ThrowsExceptionWhenJsonNotRecognised()
        {
            // Arrange
            var obj = new TestEventContainer { JoystickEvent = new InvalidJoystickEvent() };
            var json = JsonConvert.SerializeObject(obj);

            // Act & assert
            var ex = Should.Throw<Exception>(() =>
            {
                _ = JsonConvert.DeserializeObject<TestEventContainer>(json, 
                    new JsonConverter[] { 
                        new JoystickEventJsonConverter()
                    });    
            });
        }

        [Fact]
        public void Create_CreatesA_ButtonValueChangedEvent()
        {
            // Arrange
            var json = "{\"ButtonIndex\":3,\"Value\":true,\"GameAction\":\"MyGameAction\"}";
            
            // Act
            var e = JsonConvert.DeserializeObject<ButtonValueChangedEvent>(json, 
                    new JsonConverter[] { 
                        new JoystickEventJsonConverter()
                    });    

            // Assert
            e.GameAction.ShouldBe("MyGameAction");
            e.Value.Value.ShouldBeTrue();
            e.ButtonIndex.ShouldBe(3);
            e.GameActions.ShouldBeNull();
        }

        [Fact]
        public void Create_CreatesA_ButtonValueChangedEvent_WithMultipleGameActions()
        {
            // Arrange
            var json = "{\"ButtonIndex\":3,\"Value\":true,\"GameActions\":[\"GameAction1\",\"GameAction2\"]}";
            
            // Act
            var e = JsonConvert.DeserializeObject<ButtonValueChangedEvent>(json, 
                    new JsonConverter[] { 
                        new JoystickEventJsonConverter()
                    });    

            // Assert
            e.GameAction.ShouldBe("GameAction1");
            e.Value.Value.ShouldBeTrue();
            e.ButtonIndex.ShouldBe(3);
            e.GameActions.Count.ShouldBe(2);
            e.GameActions[0].ShouldBe("GameAction1");
            e.GameActions[1].ShouldBe("GameAction2");
        }
    }
}