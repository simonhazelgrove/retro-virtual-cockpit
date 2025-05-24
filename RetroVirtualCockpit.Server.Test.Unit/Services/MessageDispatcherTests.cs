using NSubstitute;
using System.Collections.Generic;
using WindowsInput;
using WindowsInput.Native;
using Xunit;
using RetroVirtualCockpit.Server.Data;
using RetroVirtualCockpit.Server.Messages;
using RetroVirtualCockpit.Server.Services;

namespace RetroVirtualCockpit.Server.Test.Unit.Services
{
    public class MessageDispatcherTests
    {
        public readonly MessageDispatcher _messagePlayer;

        private readonly InputSimulator _inputSimulator;

        private readonly IKeyboardSimulator _mockKeyboardSimulator;

        private readonly IConfigService _mockConfigService;

        public MessageDispatcherTests()
        {
            _mockConfigService = Substitute.For<IConfigService>();

            _mockKeyboardSimulator = Substitute.For<IKeyboardSimulator>();
            _inputSimulator = new InputSimulator(_mockKeyboardSimulator, null, null);

            _messagePlayer = new MessageDispatcher(_mockConfigService, _inputSimulator);
        }

        [Fact]
        public void Dispatch_DoesntProcessKeysWhenNoGameConfigIsSelected()
        {
            _messagePlayer.Dispatch("Config1.Key1", null);

            _mockKeyboardSimulator.DidNotReceive().KeyDown(Arg.Any<VirtualKeyCode>());
        }

        [Fact]
        public void Dispatch_ShouldHandleSwitchConfigsMessage()
        {
            // Arrange
            _mockConfigService.GetGameConfig("Config1").Returns(
                new GameConfig { GameActionMappings = new Dictionary<string, GameActionMapping>() 
                    {{ "Key1", new GameActionMapping { new KeyboardMessage { Key = KeyCode._1 } } } }});

            _mockConfigService.GetGameConfig("Config2").Returns(
                new GameConfig { GameActionMappings = new Dictionary<string, GameActionMapping>() 
                    {{ "Key1", new GameActionMapping { new KeyboardMessage { Key = KeyCode.A } } } }});

            // Act & Assert
            _messagePlayer.Dispatch("SetConfig:Config1", (m) => { });
            _mockKeyboardSimulator.DidNotReceive().KeyDown(Arg.Any<VirtualKeyCode>());

            // Act & Assert
            _messagePlayer.Dispatch("Key1", (m) => { });
            _mockKeyboardSimulator.Received().KeyDown(Arg.Is<VirtualKeyCode>(k => k == VirtualKeyCode.VK_1));

            // Act & Assert
            _mockKeyboardSimulator.ClearReceivedCalls();
            _messagePlayer.Dispatch("SetConfig:Config2", (m) => { });
            _mockKeyboardSimulator.DidNotReceive().KeyDown(Arg.Any<VirtualKeyCode>());
            
            // Act & Assert
            _messagePlayer.Dispatch("Key1", (m) => { });
            _mockKeyboardSimulator.Received().KeyDown(Arg.Is<VirtualKeyCode>(k => k == VirtualKeyCode.VK_A));
        }

        private List<GameConfig> GetTestConfigs()
        {
            return new List<GameConfig>
            {
                new GameConfig
                {
                    Title = "Config1",
                    GameActionMappings = new Dictionary<string, GameActionMapping>
                    {
                        { "Key1", new GameActionMapping { new KeyboardMessage { Key = KeyCode._1 } } }
                    }
                },
                new GameConfig
                {
                    Title = "Config2",
                    GameActionMappings = new Dictionary<string, GameActionMapping>
                    {
                        { "Key1", new GameActionMapping { new KeyboardMessage { Key = KeyCode.A } } }
                    }
                }
            };
        }
    }
}
