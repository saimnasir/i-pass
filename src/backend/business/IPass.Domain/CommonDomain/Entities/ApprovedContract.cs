using Patika.Shared.Entities;
using System;

namespace IPass.Domain.CommonDomain.Entities
{
    public class ApprovedContract : GenericEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ContractId { get; set; }
        public string ContractByte { get; set; }
        public string ApprovedContractByte { get; set; }
    }
}
