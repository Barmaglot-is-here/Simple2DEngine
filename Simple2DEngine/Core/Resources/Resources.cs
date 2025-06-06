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

    public static void Scan(string directory, bool trimRootDirectory = true)
    {
        string? rootDirectory = !trimRootDirectory 
                              ? Path.GetDirectoryName(directory) 
                              : directory;

        Scan(directory, rootDirectory);
    }

    private static void Scan(string directory, string? rootDirectory)
    {
        foreach (string path in Directory.EnumerateFiles(directory))
        {
            string fullPath     = path.ToLower();
            string relativePath = rootDirectory == null 
                                ? fullPath
                                : Path.GetRelativePath(rootDirectory, fullPath);

            _index[relativePath] = fullPath;
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

        path = GetPath(path);

        if (!Path.Exists(path))
            throw new ResourceLoadingException($"File doesn't exists: {path}");

        return ((IResourceLoader<T>)loader).Load(path);
    }

    public static string GetPath(string relativePath)
    {
        relativePath = relativePath.ToLower();

#if DEBUG
        if (!_index.ContainsKey(relativePath))
            throw new Exception($"{relativePath} doesn't indexed");
#endif

        return _index[relativePath];
    }
}