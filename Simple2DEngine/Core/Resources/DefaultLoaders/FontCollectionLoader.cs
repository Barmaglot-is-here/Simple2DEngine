using Simple2DEngine.Graphics;

namespace Simple2DEngine;

internal class FontCollectionLoader : IResourceLoader<FontCollection>
{
    private readonly Renderer _renderer;

    public FontCollectionLoader(Renderer renderer)
    {
        _renderer = renderer;
    }

    public FontCollection Load(string path)
    {
        string[] paths = File.ReadAllLines(path);
        MakeAbsolute(paths);

#if DEBUG
        ValidatePaths(paths);
#endif

        return FontCollection.Load(_renderer, paths);
    }

    private void MakeAbsolute(string[] paths)
    {
        for (int i = 0; i < paths.Length; i++)
            paths[i] = Resources.GetResourcePath(paths[i]);
    }

#if DEBUG
    private void ValidatePaths(string[] paths)
    {
        foreach (var path in paths)
            if (!File.Exists(path))
                throw new ResourceLoadingException($"Font file doesn't exists: {path}");
    }
#endif
}