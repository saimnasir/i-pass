using System.Collections.Generic;

namespace IPass.Shared.DTO.Common
{
    public class GetCampaignsOutputResponse : Patika.Shared.DTO.DTO
    {
        public int TotalCount { get; set; }
        public ICollection<CampaignsOutputResponse> Data { get; set; }
    }
}
