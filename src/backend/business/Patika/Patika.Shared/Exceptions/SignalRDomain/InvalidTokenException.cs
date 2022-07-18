namespace Patika.Shared.Exceptions.SignalRDomain
{
	public class InvalidTokenException : ApplicationException
	{
		public InvalidTokenException() : base("SIGNAL:0001", "Token is invalid")
		{
		}
	}
}