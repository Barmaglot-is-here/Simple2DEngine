using System.Diagnostics.CodeAnalysis;
using Vortice.XAudio2;

namespace Simple2DEngine.Audio;

public class Sound : BaseSound
{
    private readonly SoundBufferManager _bufferManager;

    [AllowNull]
    private Task _loadingTask;
    [AllowNull]
    private CancellationTokenSource _cs;

    private Sound(IXAudio2SourceVoice source, Stream stream, int bitsPerSample) 
        : base(source, stream.GetRemaningLenght(), bitsPerSample)
    {
        _bufferManager  = new(stream);
    }

    public static Sound Load(SoundEngine engine, string path)
    {
        Stream stream = engine.Decode(path, out SoundFormat format);

        return Load(engine, format, stream);
    }

    public static Sound Load(SoundEngine engine, SoundFormat format, Stream stream)
    {
        var source = engine.CreateSource(format.Source);

        return new(source, stream, format.Source.BitsPerSample);
    }

    protected override void OnPlay()
    {
        base.OnPlay();

        _cs             = new();
        _loadingTask    = Task.Run(LoadBuffer, _cs.Token);
    }

    protected override void OnPause()
    {
        base.OnPause();

        _loadingTask.SafeCancel(_cs);
    }

    private void LoadBuffer()
    {
        while (!_cs!.IsCancellationRequested && !_bufferManager.EndOfStream)
        {
            if (Source.State.BuffersQueued == _bufferManager.MaxBuferCount)
                continue;

            var buffer = _bufferManager.CreateBufer();

            Source.SubmitSourceBuffer(buffer);
        }
    }

    protected override void OnDispose()
    {
        _bufferManager  .Dispose();
        _loadingTask    .SafeCancel(_cs);

        base.OnDispose();
    }
}