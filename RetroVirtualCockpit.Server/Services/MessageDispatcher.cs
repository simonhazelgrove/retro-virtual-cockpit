using System;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RetroVirtualCockpit.Server.Data;
using RetroVirtualCockpit.Server.Dispatchers;
using RetroVirtualCockpit.Server.Messages;
using RetroVirtualCockpit.Server.Receivers.Joystick;
using RetroVirtualCockpit.Server.Receivers.Mouse;

namespace RetroVirtualCockpit.Server.Services
{
    public class MessageDispatcher : IMessageDispatcher
    {
        public ServiceLifetime Lifetime => ServiceLifetime.Singleton;

        private GameConfig _selectedGameConfig;

        private IKeyboardDispatcher _keyboardDispatcher;

        private IMouseDispatcher _mouseDispatcher;

        private IConfigService _configService;

        private JsonConverter[] _jsonConverters;

        public MessageDispatcher(IConfigService configService, IKeyboardDispatcher keyboardDispatcher, IMouseDispatcher mouseDispatcher)
        {
            _configService = configService;

            _keyboardDispatcher = keyboardDispatcher;
            _mouseDispatcher = mouseDispatcher;

            _jsonConverters =
            [
                new JoystickEventJsonConverter(),
                new MouseEventJsonConverter(),
                new MessageJsonConverter()
            ];
        }

        public void Dispatch(string message, Action<GameConfig> setSelectedGameConfig)
        {
            if (message.StartsWith("["))
            {
                ProcessJsonMessageArray(message);
            }
            else if (message.StartsWith("SetConfig:"))
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
                ProcessHardCodedGameActions(message);
            }
        }

        private void ProcessJsonMessageArray(string messagesJson)
        {
            SanatizeMessage(ref messagesJson);

            try
            {
                var messages = JsonConvert.DeserializeObject<Message[]>(messagesJson, _jsonConverters);

                foreach (var message in messages)
                {
                    if (message != null)
                    {
                        Dispatch(message);
                    }
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error parsing JSON message '{messagesJson}': {ex.Message}");
            }
        }

        private void ProcessHardCodedGameActions(string gameAction)
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
        }

        private void SanatizeMessage(ref string gameAction)
        {
            for(var i = 0; i < gameAction.Length; i++)
            {
                if (gameAction[i] < ' ' || gameAction[i] > '~')
                {
                    gameAction = gameAction.Substring(0, i);
                    break;
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