using Vortice.Direct2D1;
using Vortice.Mathematics;

namespace Simple2DEngine.Graphics;

public abstract class ColorPalette : DisposableResourcesCollection
{
    internal abstract ID2D1SolidColorBrush GetBrush(Color color);
    internal abstract ID2D1Effect GetEffect(Color color);

    public abstract void Clear();
}