using Vortice.Direct2D1;
using Vortice.Mathematics;

namespace Simple2DEngine.Graphics;

public partial class Renderer : IDisposable
{
    public void DrawText(string text, Font font, Rect layoutRect, Color color)
    {
        var brush = ColorPalette.GetBrush(color);

        _context.DrawText(text, font.Source, layoutRect, brush);
    }

    public void DrawSprite(Sprite sprite, Rect? destinationRectangle = null, 
                           float opacity = 1) 
        => _context.DrawBitmap(sprite.Source, destinationRectangle, opacity, 
                               BitmapInterpolationMode.Linear, null);

    public void ClearColor(Color color) => _context.Clear(color);
}