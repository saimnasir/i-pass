using Patika.EF.Shared;
using Patika.Shared.Entities;
using System;

namespace IPass.Domain.PasswordDomain.Entities
{
    //[Auditable]
    public class Memory : GenericEntity<Guid> 
    {
        /// <summary>
        /// Organization relation
        /// </summary>
        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// MemoryType relation
        /// </summary>
        public Guid MemoryTypeId { get; set; }
        public virtual MemoryType MemoryType { get; set; }

        /// <summary>
        /// EnvironmentType relation
        /// </summary>
        public Guid? EnvironmentTypeId { get; set; }
        public virtual EnvironmentType EnvironmentType { get; set; }

        public string Title { get; set; }
        public string UserName { get; set; }
        public bool IsUserNameSecure { get; set; }
        public string Email { get; set; }
        public bool IsUEmailSecure { get; set; }
        public string HostOrIpAddress { get; set; }
        public bool IsHostOrIpAddressSecure { get; set; }
        public string Port { get; set; }
        public bool IsPortSecure { get; set; }
        public string Password { get; set; } 
        public string Description { get; set; } 
    }
}
