using Simple2DEngine.Audio;
using Simple2DEngine.Graphics;
using Simple2DEngine.Input.Keyboard;
using Vortice.Mathematics;

namespace Simple2DEngine.Samples;

public class PlaySoundSample : Application
{
    private readonly Sound _sound;
    private readonly Color _backgroundColor;

    public PlaySoundSample()
    {
        Resources.Scan(Environment.CurrentDirectory + "\\PlaySound\\Gamedata");

        _backgroundColor = new(60, 60, 60);

        _sound = Resources.Load<Sound>("SampleSound.mp3");
    }

    protected override void Draw()
    {
        Renderer.ClearColor(_backgroundColor);
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