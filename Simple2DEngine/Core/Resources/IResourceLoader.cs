namespace Simple2DEngine;

public interface IResourceLoader<T>
{
    T Load(string path);
}