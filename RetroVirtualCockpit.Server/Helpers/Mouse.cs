using System.Runtime.InteropServices;
using System.Drawing;

namespace RetroVirtualCockpit.Server.Helpers
{
    public class Mouse
    {
        // We need to use unmanaged code
        [DllImport("user32.dll")]

        // GetCursorPos() makes everything possible
        public static extern bool GetCursorPos(ref Point lpPoint);
    }
}