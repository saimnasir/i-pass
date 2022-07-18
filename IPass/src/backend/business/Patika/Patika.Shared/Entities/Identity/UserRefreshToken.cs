using System;

namespace Patika.Shared.Entities.Identity
{
    public class UserRefreshToken : Entity
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}