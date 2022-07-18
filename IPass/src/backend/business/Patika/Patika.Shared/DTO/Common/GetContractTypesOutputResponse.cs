using System.Collections.Generic;

namespace IPass.Shared.DTO.Common
{
    public class GetContractTypesOutputResponse : Patika.Shared.DTO.DTO
    {
        public ICollection<ContractTypeOutputResponse> ContractTypes { get; set; }
    }
}
