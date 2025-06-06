using SharpGen.Runtime;
using Vortice.DirectWrite;

namespace Simple2DEngine.Graphics;

internal class FontCollectionEnumerator : CallbackBase, IDWriteFontFileEnumerator
{
    private readonly IDWriteFontFile[] _files;
    private int _pos;

    internal FontCollectionEnumerator(IDWriteFontFile[] files)
    {
        _files  = files;
        _pos    = -1;
    }

    public IDWriteFontFile GetCurrentFontFile() => _files[_pos];

    public RawBool MoveNext()
    {
        var result = _pos < _files.Length;

        if (result)
            _pos++;

        return result;
    }
}