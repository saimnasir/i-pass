using System;
using System.Collections.Generic;

namespace IPass.Shared.DTO.Common
{
    public class AcceptNewContractsInputDto : Patika.Shared.DTO.DTO
    {
        public ICollection<Guid> ContractIds { get; set; }
    }
}
