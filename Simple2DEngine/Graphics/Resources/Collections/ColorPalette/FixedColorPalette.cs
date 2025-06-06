using System.Collections.Frozen;
using Vortice.Direct2D1;
using Vortice.Mathematics;

namespace Simple2DEngine.Graphics;

public sealed class FixedColorPalette : ColorPalette
{
    private readonly FrozenDictionary<Color, ID2D1SolidColorBrush> _brushes;
    private readonly FrozenDictionary<Color, ID2D1Effect> _effects;

    protected override IEnumerable<IDisposable> Values => _brushes.Values;

    public FixedColorPalette(Renderer renderer, IEnumerable<Color> colors)
    {
        colors.ToPalette(renderer, out var brushes, out var effects);

        _brushes = brushes.ToFrozenDictionary();
        _effects = effects.ToFrozenDictionary();
    }

    internal override ID2D1SolidColorBrush GetBrush(Color color) => _brushes[color];
    internal override ID2D1Effect GetEffect(Color color) => _effects[color];

    public override void Clear() 
        => throw new NotSupportedException("Can't clear FixedColorPalette");
}