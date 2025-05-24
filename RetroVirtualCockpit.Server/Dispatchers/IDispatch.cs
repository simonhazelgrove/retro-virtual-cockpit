using RetroVirtualCockpit.Server.Messages;

namespace RetroVirtualCockpit.Server.Dispatchers
{
    public interface IDispatch<T> where T : Message
    {
        void Dispatch(T message);
    }
}
