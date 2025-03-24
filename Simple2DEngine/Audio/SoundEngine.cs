using Vortice.Multimedia;
using Vortice.XAudio2;

namespace Simple2DEngine.Audio;

public class SoundEngine : IDisposable
{
    private readonly IXAudio2 _xAudio;
    private readonly IXAudio2MasteringVoice _masteringVoice;

    private readonly DecoderFactory _decoderFactory;

    public float MasterVolume 
    {
        get => _masteringVoice.Volume;
        set => _masteringVoice.SetVolume(value);
    }

    public SoundEngine()
    {
        _xAudio             = XAudio2.XAudio2Create();
        _masteringVoice     = _xAudio.CreateMasteringVoice();
        _decoderFactory     = new();

        _xAudio.StartEngine();
    }

    public void AddDecoder(string fileExtension, ISoundDecoder decoder)
        => _decoderFactory.Add(fileExtension, decoder);

    internal Stream Decode(string sound, out SoundFormat format)
    {
        string extension        = Path.GetExtension(sound);
        ISoundDecoder decoder   = _decoderFactory.Get(extension);

        return decoder.Decode(sound, out format);
    }

    internal IXAudio2SourceVoice CreateSource(WaveFormat waveFormat)
    {
        var voice = _xAudio.CreateSourceVoice(waveFormat, false);

        return voice;
    }

    public void Enable() => _xAudio.StartEngine();
    public void Disable() => _xAudio.StopEngine();

    public void Dispose()
    {
        _xAudio         .Dispose();
        _masteringVoice .Dispose();
    }
}