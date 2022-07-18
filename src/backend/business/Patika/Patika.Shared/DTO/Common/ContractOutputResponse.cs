using System;

namespace IPass.Shared.DTO.Common
{
    public class ContractOutputResponse
    {
        public Guid Id { get; set; }
        public ContractTypeOutputResponse ContractType { get; set; }
        public string Version { get; set; }
        public string Content { get; set; }
    }
}
