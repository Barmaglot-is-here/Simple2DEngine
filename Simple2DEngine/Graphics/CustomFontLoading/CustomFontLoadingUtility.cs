using DirectWrite = SharpDX.DirectWrite;

namespace Simple2DEngine.Graphics.CustomFontLoading;

//Класс-костыль, который я был вынужден написать
//Ибо аналогичный функционал в Vortice не работает
//А лезть в исходники и что-то чинить мне лень
internal class CustomFontLoadingUtility : IDisposable
{
    private readonly DirectWrite.Factory _writeFactory;

    public CustomFontLoadingUtility()
    {
        _writeFactory = new();
    }

    public IntPtr Load(string[] paths)
    {
        FontCollectionLoader collectionLoader = new(paths);

        _writeFactory.RegisterFontCollectionLoader(collectionLoader);
        DirectWrite.FontCollection fontCollection = new(_writeFactory, collectionLoader,
                                                        new());

        _writeFactory.UnregisterFontCollectionLoader(collectionLoader);

        collectionLoader.Dispose();

        return fontCollection.NativePointer;
    }

    public void Dispose() => _writeFactory.Dispose();
}