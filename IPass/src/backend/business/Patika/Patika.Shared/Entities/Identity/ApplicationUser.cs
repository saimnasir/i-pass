using Microsoft.AspNetCore.Identity;
using System;

namespace Patika.Shared.Entities.Identity
{
    public class ApplicationUser : IdentityUser 
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;     
		public bool IsProfileCompleted { get; set; }
        public string ActivationCode { get; set; } = string.Empty;
        public DateTime ActivationCodeExpireDate { get; set; }
        public int ActivationCodeTryCount { get; set; }
		public bool IsActivationCodeValidated { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public Guid GetGuid()
        {
            return new Guid(Id);
        }
    }
}