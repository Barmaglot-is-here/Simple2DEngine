using System.Drawing;
using System.Runtime.InteropServices;

namespace Simple2DEngine.Windowing;

public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

internal static class WinApi
{
    #region Constants
    public const int PM_REMOVE              = 0x0001;

    public const uint WS_OVERLAPPEDWINDOW   = 0xcf0000;
    public const uint WS_VISIBLE            = 0x10000000;
    public const uint WS_CAPTION            = 0x00C00000;
    public const uint WS_THICKFRAME         = 0x00040000;
    public const uint WS_EX_APPWINDOW       = 0x00040000;

    public const uint CS_DBLCLKS            = 0x0008;
    public const uint CS_OWNDC              = 0x0020;

    public const uint COLOR_WINDOW          = 5;
    public const uint COLOR_BACKGROUND      = 1;
    public const uint IDC_CROSS             = 32515;

    public const uint WM_DESTROY            = 2;
    public const uint WM_QUIT               = 0x0012;
    public const uint WM_SIZE               = 0x0005;
    public const uint WM_PAINT              = 0x0f;
    public const uint WM_DISPLAYCHANGE      = 0x007E;
    public const uint WM_MOVE               = 0x0003;
    public const uint WM_ENTERSIZEMOVE      = 0x0231;
    public const uint WM_EXITSIZEMOVE       = 0x0232;
    public const uint WM_ACTIVATE           = 0x0006;

    public const uint SW_HIDE               = 0;
    public const uint SW_NORMAL             = 1;
    public const uint SW_MAXIMIZE           = 3;
    public const uint SW_MINIMIZE           = 6;
    public const uint SW_RESTORE            = 9;

    public const int GWL_STYLE              = -16;
    public const int GWL_EXSTYLE            = -20;

    public static IntPtr HWND_TOP           = IntPtr.Zero;

    public const uint MONITOR_DEFAULTTONEAREST    = 0x00000002;
    public const uint SWP_NOOWNERZORDER           = 0x0200;
    public const uint SWP_FRAMECHANGED            = 0x0020;
    #endregion

    #region Structs
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct WNDCLASSEXW
    {
        [MarshalAs(UnmanagedType.U4)]
        public int cbSize;
        [MarshalAs(UnmanagedType.U4)]
        public int style;
        public IntPtr lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        public string? lpszMenuName;
        public string lpszClassName;
        public IntPtr hIconSm;
    }

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

    public struct WINDOWPLACEMENT
    {
        public uint length;
        public uint flags;
        public uint showCmd;
        public Point ptMinPosition;
        public Point ptMaxPosition;
        public Rectangle rcNormalPosition;
        public Rectangle rcDevice;
    }

    public struct MONITORINFO
    {
        public uint cbSize;
        public Rectangle rcMonitor;
        public Rectangle rcWork;
        public uint dwFlags;
    }
    #endregion

    #region Functions
    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern UInt16 RegisterClassExW(ref WNDCLASSEXW lpWndClass);

    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern IntPtr LoadCursorW(IntPtr hInstance, int lpCursorName);

    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr CreateWindowExA(
       uint dwExStyle,
       UInt16 regResult,
       string lpWindowName,
       uint dwStyle,
       int x,
       int y,
       int nWidth,
       int nHeight,
       IntPtr hWndParent,
       IntPtr hMenu,
       IntPtr hInstance,
       IntPtr lpParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern sbyte PeekMessageW(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin,
                                            uint wMsgFilterMax, uint wRemoveMsg);

    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern IntPtr DispatchMessageW(ref MSG lpmsg);

    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern IntPtr DefWindowProcW(IntPtr hWnd, uint uMsg, IntPtr wParam, 
                                               IntPtr lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern void PostQuitMessage(int nExitCode);

    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern bool DestroyWindow(IntPtr hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern bool MoveWindow(IntPtr hWnd, int X, int Y,
    int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern bool SetWindowTextA(IntPtr hwnd, string lpString);
    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern uint GetWindowLongPtrW(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern int SetWindowLongPtrW(IntPtr hWnd, int nIndex, long dwNewLong);
    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern bool SetWindowPlacement(IntPtr hWnd, WINDOWPLACEMENT lpwndpl);

    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern IntPtr MonitorFromWindow(IntPtr hWnd, uint dwFlags);
    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern bool GetMonitorInfoW(IntPtr hMonitor, ref MONITORINFO lpmi);
    #endregion
}