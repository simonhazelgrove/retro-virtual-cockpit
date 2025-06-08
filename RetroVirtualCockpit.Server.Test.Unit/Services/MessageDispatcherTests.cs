using NSubstitute;
using RetroVirtualCockpit.Server.Dispatchers;
using RetroVirtualCockpit.Server.Data;
using RetroVirtualCockpit.Server.Messages;
using RetroVirtualCockpit.Server.Services;
using Xunit;
using Shouldly;

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
        public void Dispatch_ShouldHandleRawJsonMessage()
        {
            // Arrange
            var rawJsonMessage = "[{\"key\":\"Space\",\"action\":\"Press\"}]";

            // Act
            _messageDispatcher.Dispatch(rawJsonMessage, (m) => { });

            // Assert
            _mockConfigService.DidNotReceive().GetGameConfig(Arg.Any<string>());
            _mockKeyboardDispatcher.Received().Dispatch(Arg.Is<KeyboardMessage>(m => m.Key == KeyCode.Space && m.Action == ButtonAction.Press));
        }

        [Fact]
        public void Dispatch_ShouldHandleMultipleRawJsonMessage()
        {
            // Arrange
            var rawJsonMessage = "[{\"key\":\"Space\",\"action\":\"Press\"},{\"key\":\"Enter\",\"action\":\"Press\"}]";

            // Act
            _messageDispatcher.Dispatch(rawJsonMessage, (m) => { });

            // Assert
            Received.InOrder(() =>
            {
                _mockKeyboardDispatcher.Dispatch(Arg.Is<KeyboardMessage>(m => m.Key == KeyCode.Space && m.Action == ButtonAction.Press));
                _mockKeyboardDispatcher.Dispatch(Arg.Is<KeyboardMessage>(m => m.Key == KeyCode.Enter && m.Action == ButtonAction.Press));
            });
        }

        [Fact]
        public void Dispatch_ShouldHandleSetConfigMessage()
        {
            // Arrange
            var testConfig = new GameConfig { Title = "Config1" };
            _mockConfigService.GetGameConfig("Config1").Returns(testConfig);

            // Act
            GameConfig selectedConfig = null;
            _messageDispatcher.Dispatch("SetConfig:Config1", (c) => { selectedConfig = c; });

            // Assert
            selectedConfig.ShouldNotBeNull();
            _mockConfigService.Received().GetGameConfig("Config1");
            selectedConfig.ShouldBe(testConfig);
        }
    }
}
