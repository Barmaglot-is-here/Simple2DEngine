namespace Simple2DEngine.Audio;

public interface ISoundDecoder
{
    void Decode(string path, out SoundFormat format, out Stream stream);
}