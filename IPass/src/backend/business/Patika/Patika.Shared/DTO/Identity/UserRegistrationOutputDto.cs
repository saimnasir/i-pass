namespace Patika.Shared.DTO.Identity
{
    public class UserRegistrationOutputDto : DTO
    {
        public string Email { get; set; }
        public bool IsActivationCodeSent { get; set; }
    }
}
