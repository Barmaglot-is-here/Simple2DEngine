using SharpDX;
using SharpDX.DirectWrite;

namespace Simple2DEngine.Graphics.CustomFontLoading;

internal class FontCollection : CallbackBase, FontFileEnumerator
{
    private readonly FontFile[] _files;
    private int _pos;

    public FontFile CurrentFontFile => _files[_pos];

    internal FontCollection(FontFile[] files)
    {
        _files  = files;
        _pos    = -1;
    }

    public bool MoveNext()
    {
        var result = _pos < _files.Length;

        if (result)
            _pos++;

        return result;
    }
}