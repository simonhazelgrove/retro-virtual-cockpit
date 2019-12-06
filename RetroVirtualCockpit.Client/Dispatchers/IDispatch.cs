using RetroVirtualCockpit.Client.Messages;

namespace RetroVirtualCockpit.Client.Dispatchers
{
    public interface IDispatch<T> where T : Message
    {
        void Dispatch(T message);
    }
}
