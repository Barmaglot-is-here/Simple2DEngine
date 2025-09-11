using System.Numerics;
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

    public void DrawSprite(Sprite sprite, Vector2 position, float opacity = 1)
    {
        Rect rect = new(position, sprite.Size);

        DrawSprite(sprite, rect, opacity);
    }

    public void DrawSprite(Sprite sprite, Vector2 position, Size size, float opacity = 1)
    {
        Rect rect = new(position, size);

        DrawSprite(sprite, rect, opacity);
    }

    public void DrawSprite(Sprite sprite, Rect? destinationRectangle = null, 
                           float opacity = 1) 
        => _context.DrawBitmap(sprite.Source, destinationRectangle, opacity, 
                               BitmapInterpolationMode.Linear, null);

    public void DrawSprite(Sprite sprite, Matrix3x2 matrix, float opacity = 1)
        => DrawSprite(sprite, null, matrix, opacity);

    public void DrawSprite(Sprite sprite, Vector2 position, Matrix3x2 matrix,
                           float opacity = 1)
    {
        Rect rect = new(position, sprite.Size);

        DrawSprite(sprite, rect, matrix, opacity);
    }

    public void DrawSprite(Sprite sprite, Vector2 position, Size size, Matrix3x2 matrix,
                       float opacity = 1)
    {
        Rect rect = new(position, size);

        DrawSprite(sprite, rect, matrix, opacity);
    }

    public void DrawSprite(Sprite sprite, Rect? destinationRectangle, Matrix3x2 matrix,
                           float opacity = 1)
    {
        ApplyMatrix(matrix);

        DrawSprite(sprite, destinationRectangle, opacity);

        ApplyMatrix(Matrix3x2.Identity);
    }

    public void DrawSprite(Sprite sprite, Color color)
        => DrawSprite(sprite, Vector2.Zero, color);

    public void DrawSprite(Sprite sprite, Vector2 position, Color color)
    {
        var effect = ColorPalette.GetEffect(color);

        effect.SetInput(0, sprite.Source, new());

        _context.DrawImage(effect, position);

        effect.SetInput(0, null, new());
    }

    public void DrawSprite(Sprite sprite, Vector2 position, Size size, Color color) 
        => DrawSprite(sprite, new Rect(position, size), color);

    public void DrawSprite(Sprite sprite, Rect? destinationRectangle, Color color)
    {
        Vector2 position = destinationRectangle.HasValue
                         ? destinationRectangle.Value.Position
                         : Vector2.Zero;

        if (destinationRectangle.HasValue)
        {
            Rect rect = destinationRectangle.Value;

            Vector2 scale = new(rect.Size.Width / sprite.Size.Width,
                                rect.Size.Height / sprite.Size.Height);

            var scaleMatrix = Matrix3x2.CreateScale(scale, position);

            ApplyMatrix(scaleMatrix);
        }

        DrawSprite(sprite, position, color);

        if (_context.Transform != Matrix3x2.Identity)
            ApplyMatrix(Matrix3x2.Identity);
    }

    public void DrawSprite(Sprite sprite, Matrix3x2 matrix, Color color)
        => DrawSprite(sprite, null, matrix, color);

    public void DrawSprite(Sprite sprite, Vector2 position, Matrix3x2 matrix, Color color)
    {
        ApplyMatrix(matrix);

        DrawSprite(sprite, position, color);

        ApplyMatrix(Matrix3x2.Identity);
    }

    public void DrawSprite(Sprite sprite, Vector2 position, Size size, Matrix3x2 matrix,
                       Color color) 
        => DrawSprite(sprite, new Rect(position, size), matrix, color);

    public void DrawSprite(Sprite sprite, Rect? destinationRectangle, Matrix3x2 matrix, 
                           Color color)
    {
        ApplyMatrix(matrix);

        DrawSprite(sprite, destinationRectangle, color);

        ApplyMatrix(Matrix3x2.Identity);
    }

    private void ApplyMatrix(Matrix3x2 matrix) => _context.Transform = matrix;

    public void ClearColor(Color color) => _context.Clear(color);
}