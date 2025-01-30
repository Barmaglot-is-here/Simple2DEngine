using System.Drawing;
using System.Runtime.InteropServices;

namespace Simple2DEngine.Input;

internal static class WinApi
{
    public const int PM_REMOVE          = 0x0001;

    public const int WM_QUIT            = 0x0012;
    public const int WM_KEYDOWN         = 0x0100;
    public const int WM_KEYUP           = 0x0101;

    public const int WM_MOUSEMOVE       = 0x0200;
    public const int WM_LBUTTONDOWN     = 0x0201;
    public const int WM_LBUTTONUP       = 0x0202;
    public const int WM_LBUTTONDBLCLK   = 0x0203;
    public const int WM_RBUTTONDOWN     = 0x0204;
    public const int WM_RBUTTONUP       = 0x0205;
    public const int WM_RBUTTONDBLCLK   = 0x0206;
    public const int WM_MBUTTONUP       = 0x0207;
    public const int WM_MBUTTONDOWN     = 0x0208;
    public const int WM_MBUTTONDBLCLK   = 0x0209;
    public const int WM_XBUTTONDOWN     = 0x020B;
    public const int WM_XBUTTONUP       = 0x020C;
    public const int WM_XBUTTONDBLCLK   = 0x020D;
    public const int WM_MOUSEWHEEL      = 0x020A;
    public const int WM_MOUSEHWHEEL     = 0x020E;

    [StructLayout(LayoutKind.Sequential)]
    public struct MSG
    {
        public IntPtr hwnd;
        public uint message;
        public UIntPtr wParam;
        public IntPtr lParam;
        public int time;
        public Point pt;
        public int lPrivate;
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern sbyte PeekMessageW(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin,
        uint wMsgFilterMax, uint wRemoveMsg);

    [DllImport("User32.dll", ExactSpelling = true)]
    public static extern bool PostMessageW(IntPtr hWnd, uint msg, UIntPtr wParam, IntPtr lParam);
}