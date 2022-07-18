using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Patika.Shared.DTO.Identity
{
    public class TokenResultDto
    {
        [JsonPropertyName("accessToken")]
        [JsonProperty(propertyName: "accessToken")]
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = string.Empty;
        public long ExpiresIn { get; set; }
        public bool IsActivationCodeValidated { get; set; }
        public bool IsProfileCompleted { get; set; }
        public bool IsActivationCodeSent { get; set; }
        public bool IsContractsAccepted { get; set; }
    }
}
