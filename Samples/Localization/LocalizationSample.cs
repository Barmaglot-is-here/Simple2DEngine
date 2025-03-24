using Simple2DEngine.Audio;
using Simple2DEngine.Graphics;
using Simple2DEngine.Input.Keyboard;

namespace Simple2DEngine.Samples;

public class LocalizationSample : Application
{
    private readonly Sprite _sprite;
    private readonly Sound _sound;

    public LocalizationSample(string lang = "")
    {
        Resources.Scan(Environment.CurrentDirectory + "\\Localization\\Gamedata");
        Resources.Scan(Environment.CurrentDirectory + "\\Localization\\Localization\\" + lang);

        _sprite = Resources.Load<Sprite>("Sprite1.png");
        _sound  = Resources.Load<Sound>("SampleSound.mp3");

        _sound.Play();
    }

    protected override void Draw()
    {
        Renderer.DrawSprite(_sprite);
    }

    protected override void Update()
    {
        if (Keyboard.Pressed(Key.P))
            if (!_sound.IsPlayed)
                _sound.Play();
            else
                _sound.Pause();

        if (Keyboard.Pressed(Key.Q))
            Window.Close();

        if (Keyboard.Pressed(Key.F))
            Window.Fullscreen(!Window.IsFullscreen);
    }
}