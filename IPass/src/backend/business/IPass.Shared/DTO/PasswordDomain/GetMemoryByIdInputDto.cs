using Patika.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPass.Shared.DTO.PasswordDomain
{
    public class GetMemoryByIdInputDto : IdInputDto<Guid>
    {
        public GetMemoryByIdInputDto(Guid id) : base(id)
        {
        }

        public bool Decode { get; set; }
    }
}
