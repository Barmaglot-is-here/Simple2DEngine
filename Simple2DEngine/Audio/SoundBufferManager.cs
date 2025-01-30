using Vortice.XAudio2;

namespace Simple2DEngine.Audio;

internal class SoundBufferManager : IDisposable
{
    private readonly Queue<AudioBuffer> _buffers;
    private readonly Stream _fs;

    public readonly int MaxBuferCount;
    public readonly int BufferSize;

    public bool EndOfStream => _fs.Length <= _fs.Position;

    public SoundBufferManager(Stream stream, int maxBufferCount = 3, int bufferSize = 4096)
    {
        _fs             = stream;
        MaxBuferCount   = maxBufferCount;
        BufferSize      = bufferSize;
        _buffers        = new(maxBufferCount);
    }

    public AudioBuffer CreateBufer()
    {
        if (_buffers.Count == MaxBuferCount)
            _buffers.Dequeue().Dispose();

        return CreateBufer(BufferSize);
    }

    private AudioBuffer CreateBufer(int size)
    {
        long maxSize = _fs.Length - _fs.Position;

        if (size > maxSize)
            size = (int)maxSize;

        byte[] bytes = new byte[size];

        _fs.Read(bytes, 0, size);

        AudioBuffer buffer = new(bytes);
        _buffers.Enqueue(buffer);

        return buffer;
    }

    public void Dispose()
    {
        foreach (var buffer in _buffers)
            buffer.Dispose();

        _fs.Dispose();
    }
}