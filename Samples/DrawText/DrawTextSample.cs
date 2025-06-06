using Simple2DEngine.Graphics;
using Simple2DEngine.Input.Keyboard;
using Vortice.DirectWrite;
using Vortice.Mathematics;

namespace Simple2DEngine.Samples;

public class DrawTextSample : Application
{
    private readonly Font _font;
    private readonly Font _font2;
    private readonly Color _bakcgroundColor;
    private readonly Color _fontColor;

    private string Text = "TestText";

    public DrawTextSample()
    {
        Resources.Scan(Environment.CurrentDirectory + "\\DrawText\\Gamedata");

        var fontCollection  = Resources.Load<FontCollection>("fontCollection.list");

        _font = fontCollection.GetFont("Ubuntu", FontWeight.Bold,
                                       FontStyle.Oblique, FontStretch.Condensed, 32);
        _font2 = fontCollection.GetFont("Ubuntu", FontWeight.Thin,
                                       FontStyle.Normal, FontStretch.Condensed, 32);

        _bakcgroundColor    = new(0, 0, 0);
        _fontColor          = new(255, 255, 255);
    }

    protected override void Draw()
    {
        Renderer.ClearColor(_bakcgroundColor);

        Rect textRect   = new(300, 100);
        Rect textRect2  = new(300, 100);

        SetToCenter(ref textRect);
        SetToCenter(ref textRect2);

        textRect2.Y += 100;

        Renderer.DrawText(Text, _font, textRect, _fontColor);
        Renderer.DrawText(Text, _font2, textRect2, _fontColor);
    }

    private void SetToCenter(ref Rect rect)
    {
        var windowSize = Window.Size;

        rect.X = windowSize.X / 2 - rect.Width / 2;
        rect.Y = windowSize.Y / 2 - rect.Height / 2;
    }

    protected override void Update()
    {
        if (Keyboard.KeyPressed(Key.Q))
            Window.Close();

        if (Keyboard.KeyPressed(Key.F))
            Window.Fullscreen(!Window.IsFullscreen);

        if (Keyboard.KeyPressed(Key.F1))
            Text = "ChangedText";
    }
}