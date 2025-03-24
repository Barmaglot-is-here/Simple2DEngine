using System.Collections.Frozen;
using Vortice.Direct2D1;
using Vortice.Mathematics;

namespace Simple2DEngine.Graphics;

public sealed class FixedColorPalette : ColorPalette
{
    private readonly FrozenDictionary<Color, ID2D1SolidColorBrush> _brushes;

    protected override IEnumerable<ID2D1SolidColorBrush> Brushes => _brushes.Values;

    public FixedColorPalette(Renderer renderer, IEnumerable<Color> colors)
    {
        Dictionary<Color, ID2D1SolidColorBrush> brushes = colors.ToPalette(renderer);

        _brushes = brushes.ToFrozenDictionary();
    }

    internal override ID2D1SolidColorBrush GetBrush(Color color) => _brushes[color];

    public override void Clear() 
        => throw new NotImplementedException("Can't clear FixedColorPalette");
}