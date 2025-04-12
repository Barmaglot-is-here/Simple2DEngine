using Simple2DEngine.Graphics;
using Simple2DEngine.Input.Keyboard;
using System.Numerics;
using Vortice.Mathematics;

namespace Simple2DEngine.Samples;

public class TransformImageSample : Application
{
    private readonly Sprite _sprite;

    public TransformImageSample()
    {
        Resources.Scan(Environment.CurrentDirectory + "\\TransformImage\\Gamedata");

        _sprite = Resources.Load<Sprite>("Sprite1.png");
    }

    protected override void Draw()
    {
        Rect rect           = new(200, 200);
        Rect rect2          = new(75, 75, 200, 200);
        Matrix3x2 matrix    = Matrix3x2.CreateScale(1.75f);
        Matrix3x2 matrix2   = Matrix3x2.CreateRotation(35, rect2.Center);

        Renderer.DrawSprite(_sprite, rect, matrix);
        Renderer.DrawSprite(_sprite, rect2, matrix2);
    }
     
    protected override void Update()
    {
        if (Keyboard.Pressed(Key.Q))
            Window.Close();

        if (Keyboard.Pressed(Key.F))
            Window.Fullscreen(!Window.IsFullscreen);
    }
}