using Patika.Shared.Entities;
using Patika.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPass.Domain.CommonDomain.Entities
{
	public class User : GenericEntity<Guid>, ILogicalDelete
    {
        public User()
        {

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
		public string UserName { get; set; }
		public DateTime? BirthDate { get; set; }
        public string PhotoId { get; set; } = string.Empty;
        public virtual ICollection<OTPHistory> OTPHistories { get; set; }
        public virtual ICollection<PinCode> PinCodes { get; set; }
        public DateTime LastSeen { get; set; }
    }
}