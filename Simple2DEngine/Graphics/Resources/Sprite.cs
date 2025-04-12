using Vortice.Direct2D1;
using Vortice.Mathematics;

namespace Simple2DEngine.Graphics;

public class Sprite : IDisposable
{
    internal readonly ID2D1Bitmap Source;

    public SizeI Size => Source.PixelSize;

    public float Height => Source.PixelSize.Height;
    public float Width => Source.PixelSize.Width;

    private Sprite(ID2D1Bitmap source)
    {
        Source = source;
    }

    public static Sprite FromFile(Renderer renderer, string path)
    {
        var source = renderer.CreateBitmapFrom(path);

        return new Sprite(source);
    }

    public void Dispose() => Source.Dispose();
}