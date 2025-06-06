using Simple2DEngine.Graphics;
using Simple2DEngine.Input.Keyboard;
using Vortice.Mathematics;

namespace Simple2DEngine.Samples;

public class ColorizeImageSample : Application
{
    private readonly Sprite _sprite;

    public ColorizeImageSample()
    {
        Resources.Scan(Environment.CurrentDirectory + "\\ColorizeImage\\Gamedata");

        _sprite = Resources.Load<Sprite>("Sprite1.png");
    }

    protected override void Draw()
    {
        Rect rect   = new(400, 400);
        Rect rect2  = new(410, 0, 400, 400);
        Color color = new(0, 255, 25);

        Renderer.DrawSprite(_sprite, rect);
        Renderer.DrawSprite(_sprite, rect2, color);
    }

    protected override void Update()
    {
        if (Keyboard.KeyPressed(Key.Q))
            Window.Close();

        if (Keyboard.KeyPressed(Key.F))
            Window.Fullscreen(!Window.IsFullscreen);
    }
}