using RetroVirtualCockpit.Server.Messages;

namespace RetroVirtualCockpit.Server.Dispatchers;

public interface IMouseDispatcher : IDispatch<MouseMessage>
{
    void HandleStickMoveX(int amount);

    void HandleStickMoveY(int amount);
}