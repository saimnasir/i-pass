using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace IPass.Shared.DTO.Common.CDH
{
    public class GetCustomerByValueRequestCDHInput
    {
        [JsonPropertyName("accessToken")]
        [JsonProperty(propertyName: "accessToken")]
        public string AccessToken { get; set; }
        [JsonPropertyName("valueFilter")]
        [JsonProperty(propertyName: "valueFilter")]
        public string ValueFilter { get; set; }
        [JsonPropertyName("value")]
        [JsonProperty(propertyName: "value")]
        public string Value { get; set; }
    }
}
