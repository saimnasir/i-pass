using System.Collections.Generic;

namespace IPass.Shared.DTO.Common
{
    public class GetCampaignsForUserOutputResponse : Patika.Shared.DTO.DTO
    {
        public int TotalCount { get; set; }
        public ICollection<CampaignsForUserOutputResponse> Data { get; set; }
    }
}
