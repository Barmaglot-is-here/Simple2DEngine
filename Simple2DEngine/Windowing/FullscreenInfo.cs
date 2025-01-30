using static Simple2DEngine.Windowing.WinApi;

namespace Simple2DEngine.Windowing;

internal struct FullscreenInfo
{
    public WINDOWPLACEMENT WindowPlacement;
    public uint WindowStyle;
}