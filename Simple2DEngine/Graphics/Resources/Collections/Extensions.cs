using Vortice.Direct2D1;
using Vortice.Mathematics;

namespace Simple2DEngine.Graphics;

internal static class Extensions
{
    public static void ToPalette(this IEnumerable<Color> colors, Renderer renderer, 
        out Dictionary<Color, ID2D1SolidColorBrush> brushes,
        out Dictionary<Color, ID2D1Effect> effects)
    {
        int colorsCount = colors.Count();

        brushes = new(colorsCount);
        effects = new(colorsCount);

        foreach (var color in colors)
        {
            var brush   = renderer.CreateBrush(color);
            var effect  = renderer.CreateEffect(color);

            brushes.Add(color, brush);
            effects.Add(color, effect);
        }
    }

    public static ID2D1Effect CreateEffect(this Renderer renderer, Color color)
    {
        var effect = renderer.CreateEffect(EffectGuids.ColorMatrix);

        var colorMatrix = Utils.MatrixFromColor(color);

        effect.SetValue((uint)ColorMatrixProperties.ColorMatrix, colorMatrix);

        return effect;
    }

    public static TValue GetOrNew<TKey, TValue>(this IDictionary<TKey, TValue> dict, 
                                                TKey key,
                                                Func<TKey, TValue> createFunc)
    {
        if (!dict.TryGetValue(key, out TValue? value))
            dict[key] = value = createFunc(key);

        return value;
    }

    public static void DisposeAll(this IEnumerable<IDisposable> disposables)
    {
        foreach (var disposable in disposables)
            disposable.Dispose();
    }
}