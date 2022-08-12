namespace Patika.IdentityServer.Shared.Dto
{
    public class ApplicationUserInputDto
    {
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool TwoFactorEnabled { get; set; }
    }
}