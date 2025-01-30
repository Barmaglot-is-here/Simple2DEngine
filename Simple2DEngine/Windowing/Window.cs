using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

using static Simple2DEngine.Windowing.WinApi;

namespace Simple2DEngine.Windowing;

public class Window
{
    private string _title;
    private Point _size;
    private Point _position;

    private readonly WndProcDelegate _wndProc;

    private FullscreenInfo _fullscreenInfo;

    public string Title
    {
        get => _title;
        set => SetTitle(value);
    }

    public Point Size 
    {
        get => _size;
        set => SetSize(value);
    }
    public Point Position 
    {
        get => _position;
        set => SetPosition(value);
    }

    public IntPtr Handle { get; }

    public Action<Point>? OnSizeChanged { get; set; }
    public Action<Point>? OnPositionChanged { get; set ; }

    public Action? OnResizeBegin { get; set; }
    public Action? OnResizeEnd { get; set; }
    public Action? OnFullscreenChanhged { get; set; }

    public bool IsFullscreen { get; private set; }

    public Window() : this("Title", new(1280, 1080), new(0, 0)) { }
    public Window(string title) : this(title, new(1280, 1080), new(0, 0)) { }
    public Window(string title, Point size) : this(title, size, new(0, 0)) { }

    public Window(string title, Point size, Point position)
    {
        IntPtr handle   = Process.GetCurrentProcess().Handle;
        WNDCLASSEXW wc  = new();

        _wndProc = WndProc;

        wc.cbSize           = Marshal.SizeOf(typeof(WNDCLASSEXW));
        wc.style            = (int)(CS_DBLCLKS | CS_OWNDC);
        wc.hbrBackground    = (IntPtr)COLOR_BACKGROUND + 1;
        wc.cbClsExtra       = 0;
        wc.cbWndExtra       = 0;
        wc.hInstance        = handle;
        wc.hIcon            = IntPtr.Zero;
        wc.hCursor          = LoadCursorW(IntPtr.Zero, (int)IDC_CROSS);
        wc.lpszMenuName     = null;
        wc.lpszClassName    = title;
        wc.lpfnWndProc      = Marshal.GetFunctionPointerForDelegate(_wndProc);
        wc.hIconSm          = IntPtr.Zero;

        ushort atom = RegisterClassExW(ref wc);

        if (atom == 0)
            throw new Win32Exception(Marshal.GetLastWin32Error());

        string wndClass = wc.lpszClassName;

        IntPtr hWnd;

        hWnd = CreateWindowExA(
        WS_EX_APPWINDOW,
        atom,
        title,
        WS_OVERLAPPEDWINDOW,
        position.X, position.Y,
        size.X, size.Y,
        IntPtr.Zero,
        IntPtr.Zero,
        wc.hInstance,
        IntPtr.Zero);

        if (hWnd == ((IntPtr)0))
            throw new Win32Exception(Marshal.GetLastWin32Error());

        _title      = title;
        _size       = size;
        _position   = position;

        _fullscreenInfo = new();

        Handle = hWnd;
    }

    private IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        switch (msg)
        {
            case WM_DESTROY:
                PostQuitMessage(0);

                break;
            case WM_DISPLAYCHANGE:
            case WM_SIZE:
                int sizeParam = lParam.ToInt32();
                
                int width   = sizeParam & 0xffff;
                int height  = sizeParam >> 16;

                _size = new(width, height);
                OnSizeChanged?.Invoke(_size);

                break;
            case WM_MOVE:
                long positionParam = lParam.ToInt64();

                int x = (int)(positionParam & 0xffff);
                int y = (int)(positionParam >> 16 & 0xffff);

                _position = new(x, y);
                OnPositionChanged?.Invoke(_position);

                break;
            case WM_ENTERSIZEMOVE:
                OnResizeBegin?.Invoke();

                break;
            case WM_EXITSIZEMOVE:
                OnResizeEnd?.Invoke();

                break;
            case WM_ACTIVATE:
                if (!IsFullscreen)
                    break;

                int activate = wParam.ToInt32() & 0xffff;

                if (activate == 0)
                    Minimize();

                break;
            default:
                break;
        }

        return DefWindowProcW(hWnd, msg, wParam, lParam);
    }

    public void Update(out bool result)
    {
        while (PeekMessageW(out MSG msg, IntPtr.Zero, 0, 0, PM_REMOVE) != 0)
        {
            if (msg.message == WM_QUIT)
            {
                result = false;

                return;
            }

            DispatchMessageW(ref msg);
        }

        result = true;
    }

    public void Maximize() => ShowWindow(Handle, SW_MAXIMIZE);
    public void Minimize() => ShowWindow(Handle, SW_MINIMIZE);
    public void Restore() => ShowWindow(Handle, SW_RESTORE);
    public void Hide() => ShowWindow(Handle, SW_HIDE);
    public void Show() => ShowWindow(Handle, SW_NORMAL);
    public void Close() => DestroyWindow(Handle);

    public void Fullscreen(bool mode = true)
    {
        if (mode == IsFullscreen)
            return;

        if (mode)
        {
            long currentStyle       = _fullscreenInfo.WindowStyle 
                                    = GetWindowLongPtrW(Handle, GWL_STYLE);
            long fullscreenStyle    = currentStyle & ~(WS_CAPTION | WS_THICKFRAME);

            GetWindowPlacement(Handle, ref _fullscreenInfo.WindowPlacement);
            SetWindowLongPtrW(Handle, GWL_STYLE, fullscreenStyle);

            ToMonitorSize();
        }
        else
        {
            SetWindowPlacement(Handle, _fullscreenInfo.WindowPlacement);
            SetWindowLongPtrW(Handle, GWL_STYLE, _fullscreenInfo.WindowStyle);

            ShowWindow(Handle, _fullscreenInfo.WindowPlacement.showCmd);
        }

        IsFullscreen = mode;

        OnFullscreenChanhged?.Invoke();
    }

    private void ToMonitorSize()
    {
        IntPtr hmon     = MonitorFromWindow(Handle, MONITOR_DEFAULTTONEAREST);
        MONITORINFO mi  = new();
        mi.cbSize       = (uint)Marshal.SizeOf(mi);

        GetMonitorInfoW(hmon, ref mi);

        int x       = mi.rcMonitor.Left;
        int y       = mi.rcMonitor.Top;
        int nWidth  = mi.rcMonitor.Right - mi.rcMonitor.Left;
        int nHeight = mi.rcMonitor.Bottom - mi.rcMonitor.Top;

        MoveWindow(Handle, x, y, nWidth, nHeight, false);
    }

    public void SetTitle(string title)
    {
        SetWindowTextA(Handle, title);

        _title = title;
    }

    public void SetSize(Point size)
    {
        MoveWindow(Handle, Position.X, Position.Y, size.X, size.Y, true);

        _size = size;

        OnSizeChanged?.Invoke(size);
    }

    public void SetPosition(Point position)
    {
        MoveWindow(Handle, position.X, position.Y, Size.X, Size.Y, true);

        _position = position;

        OnPositionChanged?.Invoke(position);
    }
}