using System.Drawing;
using static Simple2DEngine.Input.WinApi;

namespace Simple2DEngine.Input.Mouse;

public static partial class Mouse
{
    private static readonly ButtonState[] _buttonState;

    public static Point Position { get; private set; }
    public static Point DeltaMove { get; private set; }

    public static int VerticalScrollWheel { get; private set; }
    public static int HorizontalScrollWheel { get; private set; }

    static Mouse()
    {
        int buttonsCount    = Enum.GetNames(typeof(MouseButton)).Length;
        _buttonState        = new ButtonState[buttonsCount];
    }

    public static bool Clicked(MouseButton button)   
        => _buttonState[(int)button] == ButtonState.Click;
    public static bool DoubleClicked(MouseButton button)   
        => _buttonState[(int)button] == ButtonState.DoubleClick;
    public static bool Holded(MouseButton button)   
        => _buttonState[(int)button] == ButtonState.Click
        || _buttonState[(int)button] == ButtonState.DoubleClick
        || _buttonState[(int)button] == ButtonState.Hold;
    public static bool Released(MouseButton button) 
        => _buttonState[(int)button] == ButtonState.Release;

    internal static void Update()
    {
        ClearState();

        while (PeekMessageW(out MSG msg, IntPtr.Zero, WM_MOUSEMOVE, WM_MOUSEHWHEEL, PM_REMOVE) != 0)
        {
            if (msg.message == WM_QUIT)
            {
                PostMessageW(IntPtr.Zero, msg.message, msg.wParam, msg.lParam);

                break;
            }

            int message = (int)msg.message;

            if (message >= WM_LBUTTONDOWN && message <= WM_MBUTTONDBLCLK)
            {
                HandleButtonClick(message);

                return;
            }

            switch (message)
            {
                case WM_XBUTTONDOWN:
                case WM_XBUTTONUP:
                case WM_XBUTTONDBLCLK:
                    HandleXButtonClick(message);

                    break;
                case WM_MOUSEMOVE:
                    HandleMovement(msg.lParam);

                    break;
                case WM_MOUSEWHEEL:
                    VerticalScrollWheel = GetWheelValue(msg.wParam);

                    break;
                case WM_MOUSEHWHEEL:
                    HorizontalScrollWheel = GetWheelValue(msg.wParam);

                    break;
            }
        }
    }

    private static void ClearState()
    {
        for (int i = 0; i < _buttonState.Length; i++)
        {
            if (_buttonState[i] == ButtonState.Click || 
                _buttonState[i] == ButtonState.DoubleClick)
                _buttonState[i] = ButtonState.Hold;
            else if (_buttonState[i] == ButtonState.Release)
                _buttonState[i] = ButtonState.None;
        }

        DeltaMove = Point.Empty;
    }

    private static void HandleButtonClick(int message)
    {
        var button  = GetButton(message);
        var state   = GetState(message);

        _buttonState[(int)button - 1] = state;
    }

    private static MouseButton GetButton(int message)
    {
        int button      = message & 0b1111;
        int buttonIndex = (button + 3) / 3;

        return (MouseButton)buttonIndex;
    }

    private static ButtonState GetState(int message)
    {
        int eventIndex = message & 0b11;

        if (eventIndex == 0b01)
            return ButtonState.Click;
        else if (message == 0b10)
            return ButtonState.Release;
        else
            return ButtonState.DoubleClick;
    }

    private static void HandleXButtonClick(int message)
    {
        var button  = GetXButton(message);
        var state   = GetXState(message);

        _buttonState[(int)button - 1] = state;
    }

    private static MouseButton GetXButton(int mouseData)
    {
        int xButton = mouseData >> 16;

        return MouseButton.Middle + xButton;
    }

    private static ButtonState GetXState(int message)
    {
        int eventIndex = message & 0b11;

        if (eventIndex == 0b11)
            return ButtonState.Click;
        if (eventIndex == 0b00)
            return ButtonState.Release;

        return ButtonState.DoubleClick;
    }

    private static void HandleMovement(IntPtr lParam)
    {
        int x = lParam.ToInt32() & 0xffff;
        int y = lParam.ToInt32() >> 16;

        var deltaX = x - Position.X;
        var deltaY = y - Position.Y;

        DeltaMove   = new(deltaX, deltaY);
        Position    = new(x, y);
    }

    private static int GetWheelValue(UIntPtr wParam) => (int)wParam >> 16;
}