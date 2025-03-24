using MP3Sharp;

namespace Simple2DEngine.Audio;

internal class Mp3Decoder : ISoundDecoder
{
    public Stream Decode(string path, out SoundFormat format)
    {
        MP3Stream mp3Stream = new(path);

        format = new(mp3Stream.Frequency, mp3Stream.ChannelCount);

        return mp3Stream;
    }
}