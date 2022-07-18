namespace Patika.Shared.DTO.Identity
{
    public class UserRegistrationOutputResponse : DTO
    {
        public string Email { get; set; }
        public bool IsActivationCodeSent { get; set; }
    }
}
