using Vortice.DirectWrite;

namespace Simple2DEngine.Graphics;

public class Font : IDisposable
{
    internal readonly IDWriteTextFormat Source;

    internal Font(IDWriteTextFormat source)
    {
        Source = source;
    }

    public void Dispose() => Source.Dispose();
}