namespace IPass.Shared.DTO.Identity.Validators
{
    public class UserNameExistanceValidatorInput : Patika.Shared.DTO.DTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
