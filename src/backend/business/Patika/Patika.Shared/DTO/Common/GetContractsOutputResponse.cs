using System.Collections.Generic;

namespace IPass.Shared.DTO.Common
{
    public class GetContractsOutputResponse : Patika.Shared.DTO.DTO
    {
        public ICollection<ContractOutputResponse> Contracts { get; set; }
    }
}
