using Vortice.DirectWrite;

namespace Simple2DEngine.Graphics;

public class FontCollection : IDisposable
{
    private readonly Renderer _renderer;
    private readonly IDWriteFontCollection _source;

    internal FontCollection(Renderer renderer, IDWriteFontCollection source)
    {
        _renderer   = renderer;
        _source     = source;
    }

    public static FontCollection Load(Renderer renderer, string[] paths)
    {
        var source = renderer.CreateFontCollection(paths);

        return new(renderer, source);
    }

    public Font GetFont(string name, FontWeight fontWeight,
                        FontStyle fontStyle, FontStretch fontStretch, int size)
    {
        var format = _renderer.CreateTextFormat(name, _source, fontWeight, fontStyle,
                                               fontStretch, size);

        return new(format);
    }

    public void Dispose() => _source.Dispose();
}