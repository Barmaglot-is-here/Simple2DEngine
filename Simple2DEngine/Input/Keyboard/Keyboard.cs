using static Simple2DEngine.Input.WinApi;

namespace Simple2DEngine.Input.Keyboard;

public static class Keyboard
{
    private static readonly KeyState[] _keyState;

    static Keyboard()
    {
        _keyState = new KeyState[256];
    }

    public static bool KeyPressed(Key key)    => _keyState[(int)key] == KeyState.Press;
    public static bool KeyHolded(Key key)     => _keyState[(int)key] == KeyState.Press 
                                           || _keyState[(int)key] == KeyState.Hold;
    public static bool KeyReleased(Key key)   => _keyState[(int)key] == KeyState.Release;

    internal static void Update()
    {
        ClearState();

        while (PeekMessageW(out MSG msg, IntPtr.Zero, WM_KEYDOWN, WM_KEYUP, PM_REMOVE) != 0)
        {
            if (msg.message == WM_QUIT)
            {
                PostMessageW(IntPtr.Zero, msg.message, msg.wParam, msg.lParam);

                break;
            }

            Key key = (Key)msg.wParam;

            switch (msg.message)
            {
                case WM_KEYUP:
                    _keyState[(int)key] = KeyState.Release;

                    break;
                case WM_KEYDOWN:
                    if (_keyState[(int)key] != KeyState.Hold)
                        _keyState[(int)key] = KeyState.Press;

                    break;
            }
        }
    }

    private static void ClearState()
    {
        for (int i = 0; i < _keyState.Length; i++)
        {
            if (_keyState[i] == KeyState.Press)
                _keyState[i] = KeyState.Hold;
            else if (_keyState[i] == KeyState.Release)
                _keyState[i] = KeyState.None;
        }
    }
}