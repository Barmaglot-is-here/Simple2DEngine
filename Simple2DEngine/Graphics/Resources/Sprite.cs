using Vortice.Direct2D1;

namespace Simple2DEngine.Graphics;

public class Sprite : IDisposable
{
    internal readonly ID2D1Bitmap Source;
    
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