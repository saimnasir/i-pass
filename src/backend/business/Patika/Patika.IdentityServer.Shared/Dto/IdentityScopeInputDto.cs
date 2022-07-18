namespace Patika.IdentityServer.Shared.Dto
{
    public class IdentityScopeInputDto
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string Description { get; set; } = "";
        public List<string> Resources { get; set; } = new List<string>();
    }
}