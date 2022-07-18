namespace Patika.IdentityServer.Shared.Dto
{
    public class ClientCreationInputDto
    {
        public string ClientId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
    }
}