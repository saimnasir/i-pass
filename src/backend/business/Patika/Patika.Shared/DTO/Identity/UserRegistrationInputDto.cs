namespace Patika.Shared.DTO.Identity
{
    public class UserRegistrationInputDto : DTO
    {       
        public string PhoneNumber { get; set; } 
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
