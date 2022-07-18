using System;
using System.Collections.Generic;

namespace IPass.Shared.DTO.Common
{
    public class GetCampaignForUserOutputResponse : Patika.Shared.DTO.DTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public ICollection<string> PhotoUrls { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime ValidTime { get; set; }
        public bool IsRead { get; set; }
    }
}
