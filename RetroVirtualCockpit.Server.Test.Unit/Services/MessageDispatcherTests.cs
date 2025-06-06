﻿using NSubstitute;
using RetroVirtualCockpit.Server.Dispatchers;
using RetroVirtualCockpit.Server.Data;
using RetroVirtualCockpit.Server.Messages;
using RetroVirtualCockpit.Server.Services;
using System.Collections.Generic;
using Xunit;

namespace RetroVirtualCockpit.Server.Test.Unit.Services
{
    public class MessageDispatcherTests
    {
        public readonly MessageDispatcher _messageDispatcher;

        private readonly IKeyboardDispatcher _mockKeyboardDispatcher;

        private readonly IMouseDispatcher _mockMouseDispatcher;

        private readonly IConfigService _mockConfigService;

        public MessageDispatcherTests()
        {
            _mockConfigService = Substitute.For<IConfigService>();

            _mockKeyboardDispatcher = Substitute.For<IKeyboardDispatcher>();
            _mockMouseDispatcher = Substitute.For<IMouseDispatcher>();

            _messageDispatcher = new MessageDispatcher(_mockConfigService, _mockKeyboardDispatcher, _mockMouseDispatcher);
        }

        [Fact]
        public void Dispatch_DoesntProcessKeysWhenNoGameConfigIsSelected()
        {
            // Act
            _messageDispatcher.Dispatch("Config1.Key1", null);

            // Assert
            _mockKeyboardDispatcher.DidNotReceive().Dispatch(Arg.Any<KeyboardMessage>());
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
            _messageDispatcher.Dispatch("SetConfig:Config1", (m) => { });
            _mockKeyboardDispatcher.DidNotReceive().Dispatch(Arg.Any<KeyboardMessage>());

            // Act & Assert
            _messageDispatcher.Dispatch("Key1", (m) => { });
            _mockKeyboardDispatcher.Received().Dispatch(Arg.Is<KeyboardMessage>(m => m.Key == KeyCode._1));

            // Act & Assert
            _mockKeyboardDispatcher.ClearReceivedCalls();
            _messageDispatcher.Dispatch("SetConfig:Config2", (m) => { });
            _mockKeyboardDispatcher.DidNotReceive().Dispatch(Arg.Any<KeyboardMessage>());
            
            // Act & Assert
            _messageDispatcher.Dispatch("Key1", (m) => { });
            _mockKeyboardDispatcher.Received().Dispatch(Arg.Is<KeyboardMessage>(m => m.Key == KeyCode.A));
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
