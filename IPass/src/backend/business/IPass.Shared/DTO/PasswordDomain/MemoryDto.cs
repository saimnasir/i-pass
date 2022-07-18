using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPass.Shared.DTO.PasswordDomain
{
    public class MemoryDto : GenericDto<Guid>
    { 
        public Guid OrganizationId { get; set; }
        public  OrganizationDto Organization { get; set; }
 
        public Guid MemoryTypeId { get; set; }
        public virtual MemoryTypeDto MemoryType { get; set; }
        public Guid? EnvironmentTypeId { get; set; }
        public EnvironmentTypeDto EnvironmentType { get; set; }

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
        public bool Active { get; set; }
    }
}
