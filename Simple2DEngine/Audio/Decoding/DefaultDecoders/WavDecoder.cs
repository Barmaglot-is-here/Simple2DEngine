using WaveDecoderSharp;

namespace Simple2DEngine.Audio;

internal class WavDecoder : ISoundDecoder
{
    public void Decode(string path, out SoundFormat format, out Stream stream)
    {
        WaveDecoder decoder     = new(path);
        WaveFormat waveFormat   = decoder.ReadFormat(out var _);

        format = new(waveFormat.SamplesPerSec, waveFormat.Channels,
                                waveFormat.AvgBytesPerSecond, waveFormat.BlockAlign,
                                waveFormat.BitsPerSample);

        stream = decoder.Stream;
    }
}