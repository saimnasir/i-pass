using IPass.Shared.DTO.PasswordDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPass.Shared.DTO.CommonDomain
{
    public class UserDto :   GenericDto<Guid>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhotoId { get; set; } = string.Empty; 
        public DateTime LastSeen { get; set; }
    }
}
