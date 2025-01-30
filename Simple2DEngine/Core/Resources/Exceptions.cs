namespace Simple2DEngine;

[Serializable]
public class ResourceLoadingException : Exception
{
	public ResourceLoadingException() { }
	public ResourceLoadingException(string message) : base(message) { }
	public ResourceLoadingException(string message, Exception inner) : base(message, inner) { }
	protected ResourceLoadingException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}