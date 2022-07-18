using Patika.Shared.Exceptions;

namespace IPass.Domain.AccountDomain.Exceptions
{
	public class MaxOTPPerDayReachedException : ApplicationException
	{
		public MaxOTPPerDayReachedException() : base("AMO:0001", "Maximum OTP Per day")
		{
		}
	}
}