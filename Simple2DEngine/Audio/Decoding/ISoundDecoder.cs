namespace Simple2DEngine.Audio;

public interface ISoundDecoder
{
    Stream Decode(string path, out SoundFormat format);
}