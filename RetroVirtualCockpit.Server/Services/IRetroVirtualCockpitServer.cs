using Microsoft.Extensions.DependencyInjection;

namespace RetroVirtualCockpit.Server.Services
{
    public interface IRetroVirtualCockpitServer
    {
        public ServiceLifetime Lifetime { get; }

        public void Start();
    }
}