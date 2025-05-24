using NSubstitute;
using RetroVirtualCockpit.Server.Dispatchers;
using RetroVirtualCockpit.Server.Messages;
using WindowsInput;
using Xunit;
using MouseButton = RetroVirtualCockpit.Server.Messages.MouseButton;

namespace RetroVirtualCockpit.Server.Test.Unit.Dispatchers
{
    public class MouseDispatcherTests
    {
        private MouseDispatcher _dispatcher;

        private IMouseSimulator _mockMouseSimulator;

        public MouseDispatcherTests()
        {
            _mockMouseSimulator = Substitute.For<IMouseSimulator>();

            _dispatcher = new MouseDispatcher(_mockMouseSimulator);
        }
        
        [Fact]
        public void Dispatch_ShouldClickLeftButton()
        {
            var message = new MouseMessage { Button = MouseButton.Left };

            _dispatcher.Dispatch(message);

            _mockMouseSimulator.Received(1).LeftButtonClick();
        }
        
        [Fact]
        public void Dispatch_ShouldClickRightButton()
        {
            var message = new MouseMessage { Button = MouseButton.Right };

            _dispatcher.Dispatch(message);

            _mockMouseSimulator.Received(1).RightButtonClick();
        }
        
        [Fact]
        public void Dispatch_ShouldDoubleClickLeftButton()
        {
            var message = new MouseMessage { Button = MouseButton.Left, Action = ButtonAction.DoublePress };

            _dispatcher.Dispatch(message);

            _mockMouseSimulator.Received(1).LeftButtonDoubleClick();
        }
        
        [Fact]
        public void Dispatch_ShouldDoubleClickRightButton()
        {
            var message = new MouseMessage { Button = MouseButton.Right, Action = ButtonAction.DoublePress };

            _dispatcher.Dispatch(message);

            _mockMouseSimulator.Received(1).RightButtonDoubleClick();
        }
        
        [Fact]
        public void Dispatch_ShouldPressDownLeftButton()
        {
            var message = new MouseMessage { Button = MouseButton.Left, Action = ButtonAction.Down };

            _dispatcher.Dispatch(message);

            _mockMouseSimulator.Received(1).LeftButtonDown();
        }
        
        [Fact]
        public void Dispatch_ShouldPressDownRightButton()
        {
            var message = new MouseMessage { Button = MouseButton.Right, Action = ButtonAction.Down };

            _dispatcher.Dispatch(message);

            _mockMouseSimulator.Received(1).RightButtonDown();
        }
        
        [Fact]
        public void Dispatch_ShouldLetUpLeftButton()
        {
            var message = new MouseMessage { Button = MouseButton.Left, Action = ButtonAction.Up };

            _dispatcher.Dispatch(message);

            _mockMouseSimulator.Received(1).LeftButtonUp();
        }
        
        [Fact]
        public void Dispatch_ShouldLetUpRightButton()
        {
            var message = new MouseMessage { Button = MouseButton.Right, Action = ButtonAction.Up };

            _dispatcher.Dispatch(message);

            _mockMouseSimulator.Received(1).RightButtonUp();
        }
    }
}