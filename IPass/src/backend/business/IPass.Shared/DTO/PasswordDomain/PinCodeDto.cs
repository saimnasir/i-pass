using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPass.Shared.DTO.PasswordDomain
{
    public class PinCodeDto : GenericDto<Guid>
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public long Expiration { get; set; }
        public bool Expired { get; set; }
    }
}
