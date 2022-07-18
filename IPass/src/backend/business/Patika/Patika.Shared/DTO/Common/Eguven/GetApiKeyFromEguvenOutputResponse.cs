using System;
using System.Collections.Generic;

namespace IPass.Shared.DTO.Common.Eguven
{
    public class GetApiKeyFromEguvenOutputResponse
    {
        public string Name { get; set; }
        public bool Error { get; set; }
        public int TpsLimit { get; set; }
        public string ApiKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<string> SignPermissionList { get; set; }
    }
}
