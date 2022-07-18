using System;
using System.Collections.Generic;

namespace IPass.Shared.DTO.Common
{
    public class RemoveCampaignInputDto : Patika.Shared.DTO.DTO
    {
        public ICollection<Guid> Ids { get; set; }
    }
}
