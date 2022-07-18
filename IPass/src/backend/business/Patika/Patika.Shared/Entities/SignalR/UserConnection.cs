using System;

namespace Patika.Shared.Entities.SignalR
{
	public class UserConnection
	{
		public string ConnectionId { get; set; } = Guid.Empty.ToString();
		public string Token { get; set; } = string.Empty;
		public string UserId { get; set; } = Guid.Empty.ToString();
		public DateTime ExpiresIn { get; set; } = DateTime.MinValue;
		public bool IsExpired() => DateTime.Now.CompareTo(ExpiresIn) > 0;
		public bool IsAuthenticated() => !IsExpired() && !string.IsNullOrEmpty(Token);
	}
}