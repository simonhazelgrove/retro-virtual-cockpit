using Microsoft.Extensions.DependencyInjection;
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
                .AddSingleton<InputSimulator>(new InputSimulator())
                .BuildServiceProvider();

            var server = serviceProvider.GetService<IRetroVirtualCockpitServer>();
            server.Start();
        }
    }
}
