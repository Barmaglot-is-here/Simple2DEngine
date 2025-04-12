using Vortice.Direct2D1;
using Vortice.Mathematics;

namespace Simple2DEngine.Graphics;

internal class ColorEffects : DisposableResourcesCollection
{
    private readonly Renderer _renderer;
    private readonly Dictionary<Color, ID2D1Effect> _effects;

    protected override IEnumerable<IDisposable> Values => _effects.Values;

    public ColorEffects(Renderer renderer) 
    {
        _renderer   = renderer;
        _effects    = new();
    }

    public ID2D1Effect Get(Color color) => _effects.GetOrNew(color, Create);

    private ID2D1Effect Create(Color color)
    {
        var effect = _renderer.CreateEffect(EffectGuids.ColorMatrix);

        var colorMatrix = Utils.MatrixFromColor(color);

        effect.SetValue((uint)ColorMatrixProperties.ColorMatrix, colorMatrix);

        return effect;
    }
}