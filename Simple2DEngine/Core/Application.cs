using Simple2DEngine.Audio;
using Simple2DEngine.Graphics;
using Simple2DEngine.Input.Keyboard;
using Simple2DEngine.Input.Mouse;
using Simple2DEngine.Windowing;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Simple2DEngine;

public class Application : IDisposable
{
    [AllowNull]
    private Task _resizeTask;
    [AllowNull]
    private CancellationTokenSource _resizeTaskCS;

    private bool _resizeRequest;

    public Window Window { get; }
    public Renderer Renderer { get; }
    public SoundEngine SoundEngine { get; }

    public Application()
    {
        Window      = new();
        Renderer    = new(Window.Handle);
        SoundEngine = new();

        Resources.AddLoader(new SpriteLoader(Renderer));
        Resources.AddLoader(new FontCollectionLoader(Renderer));
        Resources.AddLoader(new SoundLoader(SoundEngine));
        Resources.AddLoader(new SoundEffectLoader(SoundEngine));

        Window.Show();

        Window.OnSizeChanged        += OnWindowSizeChanged;
        Window.OnResizeBegin        += OnResizeBegin;
        Window.OnResizeEnd          += OnResizeEnd;
        Window.OnFullscreenChanged += OnFullscreenChanged;
    }

    public void Run()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Keyboard.Update();
            Mouse   .Update();
            Window  .Update(out isRunning);

            Tick();
        }
    }

    private void Tick()
    {
        Update();

        Renderer.BeginDraw();
        Draw();
        Renderer.EndDraw();
        Renderer.Present();
    }

    protected virtual void Update() { }
    protected virtual void Draw() { }

    private void OnWindowSizeChanged(Point size) => _resizeRequest = true;

    private void OnResizeBegin()
    {
        _resizeTaskCS   = new();
        _resizeTask     = Task.Run(ResizeTick, _resizeTaskCS.Token);
    }

    private void ResizeTick()
    {
        while (!_resizeTaskCS.IsCancellationRequested)
        {
            if (_resizeRequest)
            {
                //Изменяем рендер в соответствии с размером окна.
                //Не передаём размер,
                //Рендер самостоятельно определит размер клиентской области
                //И подстроится под неё
                Renderer.Resize();

                _resizeRequest = false;
            }

            Tick();
        }
    }

    private void OnResizeEnd()
    {
        _resizeTaskCS.Cancel();
        _resizeTaskCS.Dispose();

        _resizeTask.Wait();
        _resizeTask.Dispose();

        Renderer.Resize();
    }

    private void OnFullscreenChanged() => Renderer.Resize();

    public void Dispose()
    {
        SoundEngine .Dispose();
        Renderer    .Dispose();
    }
}