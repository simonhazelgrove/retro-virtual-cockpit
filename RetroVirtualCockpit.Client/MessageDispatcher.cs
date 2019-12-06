using RetroVirtualCockpit.Client.Data;
using RetroVirtualCockpit.Client.Dispatchers;
using RetroVirtualCockpit.Client.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using WindowsInput;
using Message = RetroVirtualCockpit.Client.Messages.Message;

namespace RetroVirtualCockpit.Client
{
    public class MessageDispatcher
    {
        private readonly List<GameConfig> _gameConfigs;

        private GameConfig _selectedGameConfig;

        private KeyboardDispatcher _keyboardDispatcher;

        public MessageDispatcher(List<GameConfig> gameConfigs, InputSimulator inputSimulator)
        {
            _gameConfigs = gameConfigs;

            _keyboardDispatcher = new KeyboardDispatcher(inputSimulator);
        }

        public void Dispatch(Message message, Action<GameConfig> setSelectedGameConfig)
        {
            if (message.MessageText.StartsWith("SetConfig:"))
            {
                var title = message.MessageText.Substring("SetConfig:".Length);
                _selectedGameConfig = _gameConfigs.FirstOrDefault(c => c.Title == title);

                setSelectedGameConfig(_selectedGameConfig);
                _keyboardDispatcher.SelectedGameConfig = _selectedGameConfig;

                var logMessage = _selectedGameConfig == null ? $"Unknown game config {title}" : $"Selected game config {title}";
                Console.WriteLine(logMessage);
            }
            else if (_selectedGameConfig == null)
            {
                Console.WriteLine($"No game config selected, ignoring message '{message.MessageText}'");
            }
            else if (message is KeyboardMessage)
            {
                _keyboardDispatcher.Dispatch(message as KeyboardMessage);
            }
            else
            {
                Console.WriteLine($"Unknown message type '{message.GetType().Name}'");
            }
        }
    }
}
