using SharpDX;
using SharpDX.DirectWrite;

namespace Simple2DEngine.Graphics.CustomFontLoading;

internal class FontCollectionLoader : CallbackBase, SharpDX.DirectWrite.FontCollectionLoader
{
    private readonly string[] _paths;

    public FontCollectionLoader(string[] paths)
    {
        _paths = paths;
    }

    public FontFileEnumerator CreateEnumeratorFromKey(Factory factory, DataPointer collectionKey)
    {
        FontFile[] files = new FontFile[_paths.Length];

        for (int i = 0; i < _paths.Length; i++)
            files[i] = new(factory, _paths[i]);

        return new FontCollection(files);
    }
}