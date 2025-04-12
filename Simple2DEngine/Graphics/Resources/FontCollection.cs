using Vortice.DirectWrite;

namespace Simple2DEngine.Graphics;

public class FontCollection : IDisposable
{
    private readonly Renderer _renderer;
    private readonly IDWriteFontCollection? _source;

    internal FontCollection(Renderer renderer, IDWriteFontCollection? source)
    {
        _renderer   = renderer;
        _source     = source;
    }

    public static FontCollection Load(Renderer renderer, string[] paths)
    {
        var source = renderer.CreateFontCollection(paths);

        return new(renderer, source);
    }

    public static FontCollection FromSystemFonts(Renderer renderer) => new(renderer, null);

    public Font GetFont(string name, int size) => GetFont(name, FontWeight.Normal,
                                                                FontStyle.Normal,
                                                                FontStretch.Normal,
                                                                size);

    public Font GetFont(string name, FontWeight fontWeight,
                        FontStyle fontStyle, FontStretch fontStretch, int size)
    {
        var format = _renderer.CreateTextFormat(name, _source, fontWeight, fontStyle,
                                               fontStretch, size);

        return new(format);
    }

    public void Dispose() => _source?.Dispose();
}