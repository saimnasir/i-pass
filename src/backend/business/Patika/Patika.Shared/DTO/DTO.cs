using Newtonsoft.Json;
using Patika.Shared.Interfaces;

namespace Patika.Shared.DTO
{
    public class DTO : IDTO, IActive
    {
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string LogId { get; set; }
        public bool Active { get; set; }
    }
}