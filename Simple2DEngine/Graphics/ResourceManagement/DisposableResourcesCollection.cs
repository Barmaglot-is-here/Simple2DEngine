namespace Simple2DEngine.Graphics;

public abstract class DisposableResourcesCollection : IDisposable
{
    protected abstract IEnumerable<IDisposable> Values { get; }

    public void Dispose() => Values.DisposeAll();
}