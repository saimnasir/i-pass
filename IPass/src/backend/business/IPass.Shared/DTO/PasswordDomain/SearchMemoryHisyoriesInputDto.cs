using Patika.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPass.Shared.DTO.PasswordDomain
{
    public class SearchMemoryHisyoriesInputDto : SearchInputDto
    {
        public bool Decode { get; set; }
        public Guid Id { get; set; }
    }
}
