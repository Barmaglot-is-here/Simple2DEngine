using MP3Sharp;
using Vortice.XAudio2;
using WaveDecoderSharp;

namespace Simple2DEngine.Audio;

internal static class Extensions
{
    public static TimeSpan CalculateDuration(this IXAudio2SourceVoice source, 
                                             long dataSize, int sampleRate)
    {
        var voiceDetails    = source.VoiceDetails;
        float seconds       = CalculateDuration(dataSize, voiceDetails.InputSampleRate,
                                                voiceDetails.InputChannels, sampleRate);

        return TimeSpan.FromSeconds(seconds);
    }

    private static float CalculateDuration(long dataSize, uint sampleRate, uint channels,
                                           int bitsPerSample)
        => dataSize / (sampleRate * channels * (bitsPerSample / 8));

    public static long GetRemaningLenght(this Stream stream) 
        => stream.Length - stream.Position;

    public static void SafeCancel(this Task task, CancellationTokenSource token)
    {
        token.Cancel();
        token.Dispose();

        task.Wait();
        task.Dispose();
    }

    public static SoundFormat ToXAudioFormat(this WaveFormat waveFormat)
        => new(waveFormat.SamplesPerSec,     waveFormat.Channels,
               waveFormat.AvgBytesPerSecond, waveFormat.BlockAlign,
               waveFormat.BitsPerSample);

    public static SoundFormat ToXAudioFormat(this MP3Stream mp3Stream)
        => new(mp3Stream.Frequency, mp3Stream.ChannelCount);
}