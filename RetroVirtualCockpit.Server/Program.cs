using Microsoft.Extensions.DependencyInjection;
using RetroVirtualCockpit.Server.Dispatchers;
using RetroVirtualCockpit.Server.Messages;
using RetroVirtualCockpit.Server.Services;
using WindowsInput;

namespace RetroVirtualCockpit.Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IRetroVirtualCockpitServer, RetroVirtualCockpitServer>()
                .AddSingleton<IConfigService, ConfigService>()
                .AddSingleton<IMessageDispatcher, MessageDispatcher>()
                .AddSingleton(new InputSimulator())
                .AddSingleton<IKeyboardDispatcher, KeyboardDispatcher>()
                .AddSingleton<IMouseDispatcher, MouseDispatcher>()                
                .BuildServiceProvider();

            var server = serviceProvider.GetService<IRetroVirtualCockpitServer>();
            server.Start();
        }
    }
}
