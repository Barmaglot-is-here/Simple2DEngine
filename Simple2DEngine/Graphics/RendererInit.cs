using SharpGen.Runtime;
using Vortice.Direct2D1;
using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.DirectWrite;
using Vortice.DXGI;
using Vortice.WIC;
using static Vortice.Direct2D1.D2D1;
using static Vortice.Direct3D11.D3D11;
using static Vortice.DirectWrite.DWrite;

namespace Simple2DEngine.Graphics;

public partial class Renderer : IDisposable
{
    private readonly IDXGIDevice _DXGIDevice;

    private readonly ID2D1Factory1 _D2DFactory;
    private readonly ID2D1Device _D2D1Device;

    private readonly IDXGIFactory2 _DXGIFactory;

    private readonly IDWriteFactory _writeFactory;
    private readonly IWICImagingFactory _WICImagingFactory;

    private readonly ID2D1DeviceContext _context;

    private readonly IDXGISwapChain _swapChain;
    private ID2D1Bitmap1 _backBuffer;

    public ColorPalette ColorPalette { get; set; }

    internal Renderer(IntPtr hwnd)
    {
        _context    = CreateContext(out _DXGIDevice, out _D2DFactory, out _D2D1Device);
        _swapChain  = CreateSwapchain(hwnd, out _DXGIFactory);
        _backBuffer = CreateBackBuffer();

        _context.Target = _backBuffer;

        Result result3 = DWriteCreateFactory(out _writeFactory!);
        result3.CheckError();

        _WICImagingFactory  = new();
        ColorPalette        = new DynamicColorPalette(this);
    }

    private ID2D1DeviceContext CreateContext(out IDXGIDevice DXGIDevice,
                                             out ID2D1Factory1 D2D1Factory,
                                             out ID2D1Device D2D1Device)
    {
        Vortice.Direct3D.FeatureLevel[] featureLevels =
        {
            Vortice.Direct3D.FeatureLevel.Level_11_1,
            Vortice.Direct3D.FeatureLevel.Level_11_0,
            Vortice.Direct3D.FeatureLevel.Level_10_1,
            Vortice.Direct3D.FeatureLevel.Level_10_0,
            Vortice.Direct3D.FeatureLevel.Level_9_3,
            Vortice.Direct3D.FeatureLevel.Level_9_2,
            Vortice.Direct3D.FeatureLevel.Level_9_1
        };

        var D3D11Device = D3D11CreateDevice(DriverType.Hardware,
                                            DeviceCreationFlags.VideoSupport |
                                            DeviceCreationFlags.BgraSupport |
                                            DeviceCreationFlags.Debug,
                                            featureLevels);

        DXGIDevice = D3D11Device.QueryInterface<IDXGIDevice>();

        D3D11Device.Release();

        Result result = D2D1CreateFactory(out D2D1Factory!);
        result.CheckError();

        D2D1Device = D2D1Factory.CreateDevice(DXGIDevice);

        return D2D1Device.CreateDeviceContext();
    }

    private IDXGISwapChain CreateSwapchain(IntPtr hwnd, out IDXGIFactory2 DXGIFactory)
    {
        SwapChainDescription1 description = new();
        description.Width               = 0;
        description.Height              = 0;
        description.Format              = Format.B8G8R8A8_UNorm;
        description.Stereo              = false;
        description.SampleDescription   = new(1, 0);
        description.BufferUsage         = Usage.RenderTargetOutput;
        description.BufferCount         = 2;
        description.Scaling             = Scaling.None;
        description.SwapEffect          = SwapEffect.FlipSequential;
        description.Flags               = SwapChainFlags.None;

        Result result = _DXGIDevice.GetAdapter(out var DXGIAdapter);
        result.CheckError();

        result = DXGIAdapter.GetParent(out DXGIFactory!);
        result.CheckError();

        DXGIAdapter.Release();

        return DXGIFactory.CreateSwapChainForHwnd(_DXGIDevice, hwnd, description);
    }

    private ID2D1Bitmap1 CreateBackBuffer()
    {
        BitmapProperties1 bitmapProperties = new();
        bitmapProperties.PixelFormat    = new(Format.B8G8R8A8_UNorm, Vortice.DCommon.AlphaMode.Ignore);
        bitmapProperties.BitmapOptions  = BitmapOptions.Target | BitmapOptions.CannotDraw;

        Result result = _swapChain.GetBuffer(0, out IDXGISurface? DXGISurface);
        result.CheckError();

        var backBuffer = _context.CreateBitmapFromDxgiSurface(DXGISurface, bitmapProperties);
        DXGISurface!.Release();

        return backBuffer;
    }
}