namespace Simple2DEngine.Audio;

[Serializable]
public class SoundLoadingException : Exception
{
	public SoundLoadingException() { }
	public SoundLoadingException(string message) : base(message) { }
	public SoundLoadingException(string message, Exception inner) : base(message, inner) { }
	protected SoundLoadingException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}