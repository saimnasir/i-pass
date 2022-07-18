using System;

namespace IPass.Shared.DTO.Common
{
    public class CreateContractInputDto : Patika.Shared.DTO.DTO
    {
        public Guid ContractTypeId { get; set; }
        public string Version { get; set; }
        public string ContractContent { get; set; }
    }
}
