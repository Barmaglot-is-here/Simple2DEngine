namespace Simple2DEngine;

public static class Resources
{
    private static readonly Dictionary<string, string> _index;
    private static readonly Dictionary<Type, object> _loaders;

    static Resources()
    {
        _index      = new();
        _loaders    = new();
    }

    public static void Scan(string directory) => Scan(directory, directory);

    private static void Scan(string directory, string rootDirectory)
    {
        foreach (string path in Directory.EnumerateFiles(directory))
        {
            string fullPath     = path.ToLower();
            string relatimePath = Path.GetRelativePath(rootDirectory, fullPath);

            _index[relatimePath] = fullPath;
        }

        foreach (string subdirectory in Directory.EnumerateDirectories(directory))
            Scan(subdirectory, rootDirectory);
    }

    public static void AddLoader<T>(IResourceLoader<T> loader) 
        => _loaders.Add(typeof(T), loader);

    public static T Load<T>(string path)
    {
        if (!_loaders.TryGetValue(typeof(T), out object? loader))
            throw new ResourceLoadingException($"{typeof(T)} loader doesn't exist");

        path = GetResourcePath(path);

        try
        {
            return ((IResourceLoader<T>)loader).Load(path);
        }
        catch (FileNotFoundException)
        {
            throw new ResourceLoadingException($"File doesn't exists: {path}");
        }
    }

    public static string GetResourcePath(string relativePath)
    {
        relativePath = relativePath.ToLower();

        return _index[relativePath];
    }
}