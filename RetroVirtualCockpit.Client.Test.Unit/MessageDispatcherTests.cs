using NSubstitute;
using NUnit.Framework;
using RetroVirtualCockpit.Client.Data;
using RetroVirtualCockpit.Client.Messages;
using System.Collections.Generic;
using WindowsInput;
using WindowsInput.Native;

namespace RetroVirtualCockpit.Client.Test.Unit
{
    [TestFixture]
    public class MessageReceiverTests
    {
        public readonly MessageDispatcher _messagePlayer;

        private readonly InputSimulator _inputSimulator;

        private readonly IKeyboardSimulator _mockKeyboardSimulator;

        public MessageReceiverTests()
        {
            _mockKeyboardSimulator = Substitute.For<IKeyboardSimulator>();

            _inputSimulator = new InputSimulator(_mockKeyboardSimulator, null, null);

            _messagePlayer = new MessageDispatcher(GetTestConfigs(), _inputSimulator);
        }

        [Test]
        public void Dispatch_DoesntProcessKeysWhenNoGameConfigIsSelected()
        {
            var message = new KeyboardMessage { MessageText = "Config1.Key1" };

            _messagePlayer.Dispatch(message, (m) => { });

            _mockKeyboardSimulator.DidNotReceive().KeyDown(Arg.Any<VirtualKeyCode>());
        }

        [Test]
        public void Dispatch_ShouldHandleSwitchConfigsMessage()
        {
            var message = new Message { MessageText = "SetConfig:Config1" };
            _messagePlayer.Dispatch(message, (m) => { });
            _mockKeyboardSimulator.DidNotReceive().KeyDown(Arg.Any<VirtualKeyCode>());

            message = new KeyboardMessage { MessageText = "Key1" };
            _messagePlayer.Dispatch(message, (m) => { });
            _mockKeyboardSimulator.Received().KeyDown(Arg.Is<VirtualKeyCode>(k => k == VirtualKeyCode.VK_1));

            _mockKeyboardSimulator.ClearReceivedCalls();
            message = new Message { MessageText = "SetConfig:Config2" };
            _messagePlayer.Dispatch(message, (m) => { });
            _mockKeyboardSimulator.DidNotReceive().KeyDown(Arg.Any<VirtualKeyCode>());
            
            message = new KeyboardMessage { MessageText = "Key1" };
            _messagePlayer.Dispatch(message, (m) => { });
            _mockKeyboardSimulator.Received().KeyDown(Arg.Is<VirtualKeyCode>(k => k == VirtualKeyCode.VK_A));
        }

        private List<GameConfig> GetTestConfigs()
        {
            return new List<GameConfig>
            {
                new GameConfig
                {
                    Title = "Config1",
                    KeyMappings = new Dictionary<string, KeyMapping>
                    {
                        { "Key1", new KeyMapping(VirtualKeyCode.VK_1) }
                    }
                },
                new GameConfig
                {
                    Title = "Config2",
                    KeyMappings = new Dictionary<string, KeyMapping>
                    {
                        { "Key1", new KeyMapping(VirtualKeyCode.VK_A) }
                    }
                }
            };
        }
    }
}
