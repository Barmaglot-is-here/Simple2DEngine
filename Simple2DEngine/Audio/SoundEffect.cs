using Vortice.XAudio2;

namespace Simple2DEngine.Audio;

public class SoundEffect : BaseSound
{
    private readonly AudioBuffer _buffer;

    private SoundEffect(IXAudio2SourceVoice source, AudioBuffer buffer, long dataSize, 
                        int bitsPerSample) : base(source, dataSize, bitsPerSample)
    {
        _buffer = buffer;

        source.SubmitSourceBuffer(_buffer);
    }

    public static SoundEffect Load(SoundEngine engine, string path)
    {
        engine.Decode(path, out SoundFormat format, out Stream stream);

        return FromStream(engine, format, stream);
    }

    public static SoundEffect FromStream(SoundEngine engine, SoundFormat format, 
                                         Stream stream)
    {
        var source = engine.CreateSource(format.Source);
        var buffer = CreateBuffer(stream);

        return new(source, buffer, stream.GetRemaningLenght(), format.Source.BitsPerSample);
    }

    private static AudioBuffer CreateBuffer(Stream stream)
    {
        long maxBufferSize = stream.Length - stream.Position;

        byte[] buffer = new byte[maxBufferSize];

        stream.Read(buffer);

        return new(buffer);
    }

    protected override void OnDispose()
    {
        base.OnDispose();

        _buffer.Dispose();
    }
}