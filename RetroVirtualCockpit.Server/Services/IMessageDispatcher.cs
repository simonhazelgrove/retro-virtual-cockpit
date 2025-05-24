using System;
using Microsoft.Extensions.DependencyInjection;
using RetroVirtualCockpit.Server.Data;

namespace RetroVirtualCockpit.Server.Services
{
    public interface IMessageDispatcher
    {
        public ServiceLifetime Lifetime { get; }

        public void Dispatch(string message, Action<GameConfig> setSelectedGameConfig);
    }
}