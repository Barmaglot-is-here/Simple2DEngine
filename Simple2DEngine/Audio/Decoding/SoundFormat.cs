using Vortice.Multimedia;

namespace Simple2DEngine.Audio;

public class SoundFormat
{
    internal WaveFormat Source { get; }

    public SoundFormat(int sampleRate, int channels)
    {
        Source = new(sampleRate, channels);
    }

    public SoundFormat(int sampleRate, int channels, int avgBytesPerSecond, 
                       int blockAlign, int bitsPerSample)
    {
        Source = WaveFormat.CreateCustomFormat
        (
            WaveFormatEncoding.Pcm,
            sampleRate,
            channels,
            avgBytesPerSecond,
            blockAlign,
            bitsPerSample
        );
    }
}