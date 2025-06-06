using SharpGen.Runtime;
using System.Drawing;
using Vortice.DXGI;

namespace Simple2DEngine.Graphics;

public partial class Renderer : IDisposable
{
    internal void BeginDraw() => _context.BeginDraw();

    internal void EndDraw()
    {
        Result result = _context.EndDraw();

        result.CheckError();
    }

    internal void Present() => _swapChain.Present(1);

    internal void Resize() => Resize(Point.Empty);
    internal void Resize(Point size)
    {
        _context.Target = null;
        _backBuffer.Release();

        var result = _swapChain.ResizeBuffers(0, (uint)size.X, (uint)size.Y, 
                                              Format.B8G8R8A8_UNorm);
        result.CheckError();

        _backBuffer     = CreateBackBuffer();
        _context.Target = _backBuffer;
    }

    public void ClearPalette() => ColorPalette.Clear();

    public void Dispose()
    {
        _DXGIDevice         .Dispose();
        _D2DFactory         .Dispose();
        _D2D1Device         .Dispose();
        _DXGIFactory        .Dispose();
        _writeFactory       .Dispose();
        _WICImagingFactory  .Dispose();
        _context            .Dispose();
        _swapChain          .Dispose();
        _backBuffer         .Dispose();
        ColorPalette        .Dispose();
    }
}