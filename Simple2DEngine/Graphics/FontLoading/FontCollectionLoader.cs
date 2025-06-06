using SharpGen.Runtime;
using Vortice.DirectWrite;

namespace Simple2DEngine.Graphics;

internal class FontCollectionLoader : CallbackBase, IDWriteFontCollectionLoader
{
    private readonly string[] _paths;

    public FontCollectionLoader(string[] paths)
    {
        _paths = paths;
    }

    public IDWriteFontFileEnumerator CreateEnumeratorFromKey(IDWriteFactory factory, nint collectionKey, uint collectionKeySize)
    {
        IDWriteFontFile[] files = new IDWriteFontFile[_paths.Length];

        for (int i = 0; i < _paths.Length; i++)
            files[i] = factory.CreateFontFileReference(_paths[i]);

        return new FontCollectionEnumerator(files);
    }
}