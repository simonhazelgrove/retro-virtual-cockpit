using Microsoft.Extensions.DependencyInjection;
using RetroVirtualCockpit.Server.Data;

namespace RetroVirtualCockpit.Server.Services
{
    public interface IConfigService
    {
        public ServiceLifetime Lifetime { get; }

        public GameConfig? GetGameConfig(string configTitle);
    }
}