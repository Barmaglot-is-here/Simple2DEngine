using Vortice.Mathematics;

namespace Simple2DEngine.Graphics;

internal static class Utils
{
    public static Matrix5x4 MatrixFromColor(Color color)
    {
        return new Matrix5x4(color.R / 255f, 0, 0, 0,
                             0, color.G / 255f, 0, 0,
                             0, 0, color.B / 255f, 0,
                             0, 0, 0, color.A / 255f,
                             0, 0, 0, 0);
    }
}