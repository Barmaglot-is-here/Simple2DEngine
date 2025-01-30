using Vortice.XAudio2;

namespace Simple2DEngine.Audio;

public abstract class BaseSound : IDisposable
{
    private bool _disposed;

    protected IXAudio2SourceVoice Source { get; }

    public float Volume { get => Source.Volume; set => Source.Volume = value; }
    public bool IsPlayed { get; private set; }

    public TimeSpan Duration { get; }
    
    protected BaseSound(IXAudio2SourceVoice source, long dataSize, int bitsPerSample)
    {
        Source      = source;
        Duration    = source.CalculateDuration(dataSize, bitsPerSample);
    }

    public void Play()
    {
        if (IsPlayed)
            return;

        Source.Start();

        IsPlayed = true;

        OnPlay();
    }

    public void Play(float volume)
    {
        Source.Volume = volume;

        Play();
    }

    public void Pause()
    {
        if (!IsPlayed)
            return;

        Source.Stop();

        IsPlayed = false;

        OnPause();
    }

    protected virtual void OnPlay() { }
    protected virtual void OnPause() { }

    public void Dispose()
    {
        if (_disposed)
            return;

        OnDispose();

        _disposed = true;
    }

    protected virtual void OnDispose() => Source.Dispose();
}