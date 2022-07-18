using IPass.Shared.DTO.CommonDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPass.Shared.DTO.PasswordDomain
{
    public class ProfileDto: Patika.Shared.DTO.DTO
    {
        public UserDto User { get; set; }
        public PinCodeDto PinCode { get; set; }
    }
}
