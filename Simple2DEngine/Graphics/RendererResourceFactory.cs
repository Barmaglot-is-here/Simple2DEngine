using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;
using Vortice.WIC;

namespace Simple2DEngine.Graphics;

public partial class Renderer : IDisposable
{
    internal ID2D1Bitmap CreateBitmapFrom(Stream stream)
    {
        var decoder = _WICImagingFactory.CreateDecoderFromStream(stream);
        var frame   = decoder.GetFrame(0);

        var formatConverter = _WICImagingFactory.CreateFormatConverter();
        formatConverter.Initialize(frame, PixelFormat.Format32bppPBGRA);

        var result = _context.CreateBitmapFromWicBitmap(formatConverter);

        decoder         .Release();
        frame           .Release();
        formatConverter .Release();

        return result;
    }

    internal ID2D1SolidColorBrush CreateBrush(Color color) 
        => _context.CreateSolidColorBrush(color);

    internal IDWriteFontCollection CreateFontCollection(string[] paths)
    {
        FontCollectionLoader collectionLoader = new(paths);
        
        _writeFactory.RegisterFontCollectionLoader(collectionLoader);
        
        var collection = _writeFactory.CreateCustomFontCollection(collectionLoader, 0, 0);

        _writeFactory.UnregisterFontCollectionLoader(collectionLoader);

        return collection;
    }

#pragma warning disable CS8604
    internal IDWriteTextFormat CreateTextFormat(string fontFamilyName, 
                                                IDWriteFontCollection? fontCollection,
                                                FontWeight fontWeight, FontStyle fontStyle, 
                                                FontStretch fontStretch, float fontSize)
    {
        var format = _writeFactory.CreateTextFormat(
            fontFamilyName, fontCollection, 
            fontWeight, fontStyle, 
            fontStretch, fontSize);

        return format;
    }
#pragma warning restore CS8604

    internal ID2D1Effect CreateEffect(Guid effectId)
    {
        IntPtr effectPtr = _context.CreateEffect(effectId);

        return new(effectPtr);
    }
}