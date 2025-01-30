using Vortice.XAudio2;

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
}