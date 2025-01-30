using Vortice.Direct2D1;
using Vortice.Mathematics;

namespace Simple2DEngine.Graphics;

internal static class Extensions
{
    public static Dictionary<Color, ID2D1SolidColorBrush> ToPalette(
        this IEnumerable<Color> colors, Renderer renderer)
    {
        Dictionary<Color, ID2D1SolidColorBrush> brushes = new(colors.Count());

        foreach (var color in colors)
        {
            var brush = renderer.CreateBrush(color);

            brushes.Add(color, brush);
        }

        return brushes;
    }

    public static void DisposeAll(this IEnumerable<IDisposable> disposables)
    {
        foreach (var disposable in disposables)
            disposable.Dispose();
    }
}