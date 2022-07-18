using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace IPass.Shared.DTO.Common.CDH
{
    public class RegisterUserToCDHServiceInput
    {
        [JsonPropertyName("CreateNewCustomerRequest")]
        [JsonProperty(propertyName: "CreateNewCustomerRequest")]
        public CreateNewCustomerRequestInput CreateNewCustomerRequest { get; set; }
    }
    public class CreateNewCustomerRequestInput
    {
        [JsonPropertyName("code")]
        [JsonProperty(propertyName: "code")]
        public string Code { get; set; }
        [JsonPropertyName("mobilePhone")]
        [JsonProperty(propertyName: "mobilePhone")]
        public string MobilePhone { get; set; }
        [JsonPropertyName("firstName")]
        [JsonProperty(propertyName: "firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("middleName")]
        [JsonProperty(propertyName: "middleName")]
        public string MiddleName { get; set; }
        [JsonPropertyName("lastName")]
        [JsonProperty(propertyName: "lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("mobilePhoneType")]
        [JsonProperty(propertyName: "mobilePhoneType")]
        public string MobilePhoneType { get; set; }
        [JsonPropertyName("mobilePhoneVerified")]
        [JsonProperty(propertyName: "mobilePhoneVerified")]
        public bool MobilePhoneVerified { get; set; }
        [JsonPropertyName("mobilePhoneActive")]
        [JsonProperty(propertyName: "mobilePhoneActive")]
        public bool MobilePhoneActive { get; set; }
        [JsonPropertyName("email")]
        [JsonProperty(propertyName: "email")]
        public string Email { get; set; }
        [JsonPropertyName("emailVerified")]
        [JsonProperty(propertyName: "emailVerified")]
        public bool EmailVerified { get; set; }
        [JsonPropertyName("accessToken")]
        [JsonProperty(propertyName: "accessToken")]
        public string AccessToken { get; set; }
    }
}
