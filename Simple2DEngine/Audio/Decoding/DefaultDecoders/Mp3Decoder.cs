using MP3Sharp;

namespace Simple2DEngine.Audio;

internal class Mp3Decoder : ISoundDecoder
{
    public void Decode(string path, out SoundFormat format, out Stream stream)
    {
        MP3Stream mp3Stream = new(path);

        format = new(mp3Stream.Frequency, mp3Stream.ChannelCount);
        stream = mp3Stream;
    }
}