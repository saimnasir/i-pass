using Newtonsoft.Json;

namespace Patika.Shared.Exceptions
{
	public class ApplicationException : BaseApplicationException
	{
		[JsonConstructor]
		public ApplicationException(string code, string message = "") : base(code, message)
		{
		}
		public ApplicationException(BaseException ex) : base(ex.Code, ex.Message)
		{
		}

		public static implicit operator ApplicationException(BaseFatalException exp) => new(exp);
	}
}