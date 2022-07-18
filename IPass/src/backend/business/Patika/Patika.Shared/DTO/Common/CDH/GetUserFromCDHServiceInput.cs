using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace IPass.Shared.DTO.Common.CDH
{
    public class GetUserFromCDHServiceInput
    {
        [JsonPropertyName("GetCustomerByValueRequest")]
        [JsonProperty(propertyName: "GetCustomerByValueRequest")]
        public GetCustomerByValueRequestCDHInput GetCustomerByValueRequest { get; set; }
    }
}
