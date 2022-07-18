using System;
using System.Collections.Generic;

namespace IPass.Shared.DTO.Common
{
    public class AddCampaignInputDto : Patika.Shared.DTO.DTO
    {
        public Guid CountyId { get; set; }
        public Guid MainCategoryId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public ICollection<string> PhotoUrls { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime ValidTime { get; set; }
        public string Link { get; set; }
    }
}
