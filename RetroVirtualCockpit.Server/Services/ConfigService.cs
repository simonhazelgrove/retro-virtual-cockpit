using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RetroVirtualCockpit.Server.Data;
using RetroVirtualCockpit.Server.Messages;
using RetroVirtualCockpit.Server.Receivers.Joystick;
using RetroVirtualCockpit.Server.Receivers.Mouse;

namespace RetroVirtualCockpit.Server.Services
{
    public class ConfigService : IConfigService
    {
        public ServiceLifetime Lifetime => ServiceLifetime.Singleton;

        private static Dictionary<string, string> _configFilenamesMap;

        public ConfigService()
        {
            LoadConfigFilenamesMap();
        }

        public GameConfig? GetGameConfig(string configTitle)
        {
            if (!_configFilenamesMap.ContainsKey(configTitle))
            {
                return null;
            }

            var config = Load(_configFilenamesMap[configTitle]);
            return config;
        }

        private void LoadConfigFilenamesMap()
        {
            _configFilenamesMap = new Dictionary<string, string>();
            var files = new DirectoryInfo("Configs").GetFiles();

            foreach(var filename in files)
            {
                var config = Load(filename.FullName);
                _configFilenamesMap.Add(config.Title, filename.FullName);
                Console.WriteLine($"Read local config {config.Title} from {filename.FullName}");
            }
        }
        
        private GameConfig Load(string filename)
        {
            var json = File.ReadAllText(filename);
            var config = JsonConvert.DeserializeObject<GameConfig>(json, 
                new JsonConverter[] { 
                    new JoystickEventJsonConverter(), 
                    new MouseEventJsonConverter(),
                    new MessageJsonConverter()
                });

            return config;
        }
    }
}