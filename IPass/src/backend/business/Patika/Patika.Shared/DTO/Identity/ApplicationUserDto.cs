using System;
using System.Collections.Generic;

namespace Patika.Shared.DTO.Identity
{
    public class ApplicationUserDto : DTO
    {
        public string Id { get; set; }
        public string UserName { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public bool IsProfileCompleted { get; set; }
        public string ActivationCode { get; set; } 
        public DateTime ActivationCodeExpireDate { get; set; }
        public int ActivationCodeTryCount { get; set; }
        public bool IsActivationCodeValidated { get; set; }
		public string PhoneNumber { get; set; }
		public bool PhoneNumberConfirmed { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public DateTime? LastLogin { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
