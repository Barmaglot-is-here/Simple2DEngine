using Simple2DEngine.Audio;

namespace Simple2DEngine;

internal class SoundEffectLoader : IResourceLoader<SoundEffect>
{
    private readonly SoundEngine _soundEngine;

    public SoundEffectLoader(SoundEngine engine)
    {
        _soundEngine = engine;
    }

    public SoundEffect Load(string path) => SoundEffect.Load(_soundEngine, path);
}