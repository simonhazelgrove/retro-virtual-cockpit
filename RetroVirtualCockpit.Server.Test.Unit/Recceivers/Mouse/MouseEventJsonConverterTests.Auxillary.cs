using RetroVirtualCockpit.Server.Receivers.Mouse;

namespace RetroVirtualCockpit.Server.Test.Unit.Recceivers.Mouse
{
    public class TestEventContainer
    {
        public IMouseEvent MouseEvent { get; set; }
    }

    public class InvalidMouseEvent : BaseMouseEvent, IMouseEvent {};
}