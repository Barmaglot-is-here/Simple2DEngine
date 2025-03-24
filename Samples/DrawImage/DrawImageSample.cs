using Simple2DEngine.Graphics;
using Simple2DEngine.Input.Keyboard;
using Vortice.Mathematics;

namespace Simple2DEngine.Samples;

public class DrawImageSample : Application
{
    private readonly Sprite _sprite;

    public DrawImageSample()
    {
        Resources.Scan(Environment.CurrentDirectory + "\\DrawImage\\Gamedata");

        _sprite = Resources.Load<Sprite>("Sprite1.png");
    }

    protected override void Draw()
    {
        Rect rect = new(200, 200);

        Renderer.DrawSprite(_sprite);
        Renderer.DrawSprite(_sprite, rect);
    }

    protected override void Update()
    {
        if (Keyboard.Pressed(Key.Q))
            Window.Close();

        if (Keyboard.Pressed(Key.F))
            Window.Fullscreen(!Window.IsFullscreen);
    }
}