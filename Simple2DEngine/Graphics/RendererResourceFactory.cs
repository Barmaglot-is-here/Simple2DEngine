using Simple2DEngine.Graphics.CustomFontLoading;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;
using Vortice.WIC;

namespace Simple2DEngine.Graphics;

public partial class Renderer : IDisposable
{
    internal ID2D1Bitmap CreateBitmapFrom(string file)
    {
        if (!Path.Exists(file))
            throw new Exception($"File doesn't exist: {file}");

        var decoder = _WICImagingFactory.CreateDecoderFromFileName(file);
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
        CustomFontLoadingUtility fontLoadingUtility = new();

        IntPtr ptr = fontLoadingUtility.Load(paths);

        fontLoadingUtility.Dispose();

        return new(ptr);
    }

    internal IDWriteTextFormat CreateTextFormat(string fontFamilyName, 
                                                IDWriteFontCollection fontCollection,
                                                FontWeight fontWeight, FontStyle fontStyle, 
                                                FontStretch fontStretch, float fontSize)
    {
        var format = _writeFactory.CreateTextFormat(
            fontFamilyName, fontCollection, 
            fontWeight, fontStyle, 
            fontStretch, fontSize);

        return format;
    }
}