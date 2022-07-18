using Patika.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPass.Domain.CommonDomain.Entities
{
    public class PinCode : GenericEntity<Guid>
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public long Expiration { get; set; }
    }
}
