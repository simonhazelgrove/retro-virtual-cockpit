using System;
using System.Linq;
using Newtonsoft.Json;
using RetroVirtualCockpit.Server.Messages;
using Shouldly;
using Xunit;

namespace RetroVirtualCockpit.Server.Test.Unit.Messages
{
    public class MessageJsonConverterTests
    {
        [Fact]
        public void Create_CreatesAllMessage()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name == "RetroVirtualCockpit.Server");

            var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Message)))
                .Where(t => t.Name != "ConfigurationMessage")  // ConfigurationMessage will never appear in JSON
                .ToList();

            types.ShouldNotBeEmpty();

            foreach(var t in types)
            {
                // Arrange
                var obj = new TestMessageContainer { Message = (Message)Activator.CreateInstance(t) };
                var json = JsonConvert.SerializeObject(obj);

                Should.NotThrow(() =>
                {
                    // Act
                    var obj2 = JsonConvert.DeserializeObject<TestMessageContainer>(json, 
                        new JsonConverter[] { 
                            new MessageJsonConverter()
                        });                

                    // Assert
                    obj2.Message.ShouldBeOfType(t);
                }, $"Couldnt create {t.Name} Message");
            }
        }

        [Fact]
        public void Create_ThrowsExceptionWhenJsonNotRecognised()
        {
            // Arrange
            var obj = new TestMessageContainer { Message = new InvalidMessage() };
            var json = JsonConvert.SerializeObject(obj);

            // Act & assert
            var ex = Should.Throw<Exception>(() =>
            {
                _ = JsonConvert.DeserializeObject<TestMessageContainer>(json, 
                    new JsonConverter[] { 
                        new MessageJsonConverter()
                    });    
            });
        }
    }
}