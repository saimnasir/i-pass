namespace Patika.Shared.DTO.Identity
{
    public class UserRegistrationInputDto : DTO
    {       
        public string UserName { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
