namespace IPass.Shared.DTO.Identity
{
    public class UpdateUserProfileInputDto : Patika.Shared.DTO.DTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
