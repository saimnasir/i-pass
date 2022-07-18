using System;
using System.Collections.Generic;

namespace IPass.Shared.DTO.Common
{
    public class CampaignsForUserOutputResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> PhotoUrls { get; set; }
        public bool IsRead { get; set; }
    }
}
