using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Server.Receivers.Mouse
{
    public interface IMouseEvent
    {
        public string GameAction { get; set; }
        
        bool Evaluate(MouseState previousState, MouseState currentState);
    }
}