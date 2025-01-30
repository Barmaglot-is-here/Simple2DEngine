using Simple2DEngine.Audio;

namespace Simple2DEngine;

internal class SoundLoader : IResourceLoader<Sound>
{
    private readonly SoundEngine _soundEngine;

    public SoundLoader(SoundEngine engine)
    {
        _soundEngine = engine;
    }

    public Sound Load(string path) => Sound.Load(_soundEngine, path);
}