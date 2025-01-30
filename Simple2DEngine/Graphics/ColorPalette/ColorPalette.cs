using Vortice.Direct2D1;
using Vortice.Mathematics;

namespace Simple2DEngine.Graphics;

public abstract class ColorPalette : IDisposable
{
    protected abstract IEnumerable<ID2D1SolidColorBrush> Brushes { get; }

    internal abstract ID2D1SolidColorBrush GetBrush(Color color);

    public abstract void Clear();

    public void Dispose() => Brushes.DisposeAll();
}