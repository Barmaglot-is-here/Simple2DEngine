using Vortice.Direct2D1;
using Vortice.Mathematics;

namespace Simple2DEngine.Graphics;

public sealed class DynamicColorPalette : ColorPalette
{
    private readonly Renderer _renderer;
    private Dictionary<Color, ID2D1SolidColorBrush> _brushes;

    protected override IEnumerable<IDisposable> Values => _brushes.Values;

    public DynamicColorPalette(Renderer renderer)
    {
        _renderer   = renderer;
        _brushes    = new();
    }

    public DynamicColorPalette(Renderer renderer, IEnumerable<Color> colors)
    {
        _renderer   = renderer;
        _brushes    = colors.ToPalette(renderer);
    }

    internal override ID2D1SolidColorBrush GetBrush(Color color)
        => _brushes.GetOrNew(color, _renderer.CreateBrush);

    public override void Clear()
    {
        Values.DisposeAll();

        _brushes = new();
    }
}