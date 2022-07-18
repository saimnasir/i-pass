using System.ComponentModel.DataAnnotations;

namespace Patika.IdentityServer.Shared.Dto
{
    public class IdentityScopeOutputDto
    {
        public string Id { get; set; } = "";

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = "";

        public string DisplayName { get; set; } = "";

        public string Description { get; set; } = "";

        public string Resources { get; set; } = "";
    }
}