using Vortice.Direct2D1;
using Vortice.Mathematics;

namespace Simple2DEngine.Graphics;

public sealed class DynamicColorPalette : ColorPalette
{
    private readonly Renderer _renderer;
    private Dictionary<Color, ID2D1SolidColorBrush> _brushes;
    private Dictionary<Color, ID2D1Effect> _effects;

    protected override IEnumerable<IDisposable> Values => _brushes.Values;

    public DynamicColorPalette(Renderer renderer)
    {
        _renderer   = renderer;
        _brushes    = new();
        _effects    = new();
    }

    public DynamicColorPalette(Renderer renderer, IEnumerable<Color> colors)
    {
        _renderer = renderer;

        colors.ToPalette(renderer, out _brushes, out _effects);
    }

    internal override ID2D1SolidColorBrush GetBrush(Color color)
        => _brushes.GetOrNew(color, _renderer.CreateBrush);
    internal override ID2D1Effect GetEffect(Color color)
        => _effects.GetOrNew(color, _renderer.CreateEffect);

    public override void Clear()
    {
        Values.DisposeAll();

        _brushes = new();
        _effects = new();
    }
}