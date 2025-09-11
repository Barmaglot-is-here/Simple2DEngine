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

    public static Sprite Load(Renderer renderer, string path)
    {
        using Stream stream = File.OpenRead(path);

        return Load(renderer, stream);
    }

    public static Sprite Load(Renderer renderer, Stream stream)
    {
        var source = renderer.CreateBitmapFrom(stream);

        return new Sprite(source);
    }

    public void Dispose() => Source.Dispose();
}