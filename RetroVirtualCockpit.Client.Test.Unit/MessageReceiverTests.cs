using NSubstitute;
using NUnit.Framework;
using RetroVirtualCockpit.Client.Data;
using System.Collections.Generic;
using WindowsInput;
using WindowsInput.Native;

namespace RetroVirtualCockpit.Client.Test.Unit
{
    [TestFixture]
    public class MessageReceiverTests
    {
        public readonly MessageReceiver _messageReceiver;

        private readonly InputSimulator _inputSimulator;

        private readonly IKeyboardSimulator _mockKeyboardSimulator;

        public MessageReceiverTests()
        {
            _mockKeyboardSimulator = Substitute.For<IKeyboardSimulator>();

            _inputSimulator = new InputSimulator(_mockKeyboardSimulator, null, null);

            _messageReceiver = new MessageReceiver(GetTestConfigs(), _inputSimulator);
        }

        [Test]
        public void InterpretMessage_DoesntProcessKeysWhenNoGameConfigIsSelected()
        {
            _messageReceiver.InterpretMessage("Config1.Key1");

            _mockKeyboardSimulator.DidNotReceive().KeyDown(Arg.Any<VirtualKeyCode>());
        }

        [Test]
        public void InterpretMessage_ShouldHandleSwitchConfigsMessage()
        {
            _messageReceiver.InterpretMessage("SetConfig:Config1");

            _mockKeyboardSimulator.DidNotReceive().KeyDown(Arg.Any<VirtualKeyCode>());

            _messageReceiver.InterpretMessage("Key1");

            _mockKeyboardSimulator.Received().KeyDown(Arg.Is<VirtualKeyCode>(k => k == VirtualKeyCode.VK_1));

            _mockKeyboardSimulator.ClearReceivedCalls();

            _messageReceiver.InterpretMessage("SetConfig:Config2");

            _mockKeyboardSimulator.DidNotReceive().KeyDown(Arg.Any<VirtualKeyCode>());

            _messageReceiver.InterpretMessage("Key1");

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
