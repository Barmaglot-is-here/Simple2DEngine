using Simple2DEngine.Graphics;

namespace Simple2DEngine;

internal class SpriteLoader : IResourceLoader<Sprite>
{
    private readonly Renderer _renderer;

    public SpriteLoader(Renderer renderer)
    {
        _renderer = renderer;
    }

    public Sprite Load(string path) => Sprite.Load(_renderer, path);
}