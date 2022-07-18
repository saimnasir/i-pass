using System;

namespace IPass.Domain.CommonDomain.Entities
{
	public class OTPHistory
	{
		public Guid UserId { get; set; }
		public DateTime SentAt { get; set; } = DateTime.Now;
	}
}