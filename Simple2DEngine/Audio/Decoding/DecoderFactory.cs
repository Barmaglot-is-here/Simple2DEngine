namespace Simple2DEngine.Audio;

internal class DecoderFactory
{
    private readonly Dictionary<string, ISoundDecoder> _decoders;

    public DecoderFactory()
    {
        _decoders = new();

        _decoders.Add(".wav", new WavDecoder());
        _decoders.Add(".mp3", new Mp3Decoder());
    }

    public void Add(string fileExtension, ISoundDecoder decoder)
        => _decoders.Add(fileExtension, decoder);

    internal ISoundDecoder Get(string fileExtension)
    {
        if (_decoders.TryGetValue(fileExtension, out var decoder))
            return decoder;

        throw new SoundLoadingException($"{fileExtension} decoder doesn't exists");
    }
}