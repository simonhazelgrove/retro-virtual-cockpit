using System;
using Microsoft.Extensions.DependencyInjection;
using RetroVirtualCockpit.Server.Data;
using RetroVirtualCockpit.Server.Dispatchers;
using RetroVirtualCockpit.Server.Messages;
using WindowsInput;

namespace RetroVirtualCockpit.Server.Services
{
    public class MessageDispatcher : IMessageDispatcher
    {
        public ServiceLifetime Lifetime => ServiceLifetime.Singleton;

        private GameConfig _selectedGameConfig;

        private IKeyboardDispatcher _keyboardDispatcher;

        private IMouseDispatcher _mouseDispatcher;

        private IConfigService _configService;

        public MessageDispatcher(IConfigService configService, IKeyboardDispatcher keyboardDispatcher, IMouseDispatcher mouseDispatcher)
        {
            _configService = configService;
            _keyboardDispatcher = keyboardDispatcher;
            _mouseDispatcher = mouseDispatcher;
        }

        public void Dispatch(string message, Action<GameConfig> setSelectedGameConfig)
        {
            if (message.StartsWith("SetConfig:"))
            {
                var title = message.Substring("SetConfig:".Length);
                _selectedGameConfig = _configService.GetGameConfig(title);

                setSelectedGameConfig(_selectedGameConfig);

                var logMessage = _selectedGameConfig == null ? $"Unknown game config {title}" : $"Selected game config {title}";
                Console.WriteLine(logMessage);
            }
            else if (_selectedGameConfig == null)
            {
                Console.WriteLine($"No game config selected, ignoring message '{message}'");
            }
            else
            {
                ProcessGameActions(message);
            }
        }

        private void ProcessGameActions(string gameAction)
        {
            if (gameAction.StartsWith("Controls.Stick.Left.") || gameAction.StartsWith("Controls.Stick.Right."))
            {
                var amount = int.Parse(gameAction.Substring(gameAction.LastIndexOf(".") + 1));
                _mouseDispatcher.HandleStickMoveX(amount);
            }
            else if (gameAction.StartsWith("Controls.Stick.Back.") || gameAction.StartsWith("Controls.Stick.Forward."))
            {
                var amount = int.Parse(gameAction.Substring(gameAction.LastIndexOf(".") + 1));
                _mouseDispatcher.HandleStickMoveY(amount);
            }
            else 
            {
                if (_selectedGameConfig.GameActionMappings.TryGetValue(gameAction, out var messages))
                {
                    Console.WriteLine($"GameAction: '{gameAction}'");

                    foreach(var message in messages)
                    {
                        Dispatch(message);
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown GameAction '{gameAction}'");
                }
            }
        }

        private void Dispatch(Message message)
        {
            switch (message)
            {
                case KeyboardMessage keyboardMessage:
                    _keyboardDispatcher.Dispatch(keyboardMessage);
                    break;
                case MouseMessage mouseMessage:
                    _mouseDispatcher.Dispatch(mouseMessage);
                    break;
                default:
                    Console.WriteLine($"Unknown message type '{message.GetType().Name}'");
                    break;
            }
        }
    }
}