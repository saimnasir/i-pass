namespace IPass.Shared.DTO.Common
{
    public class GetUserFromCDHServiceOutputResponse : Patika.Shared.DTO.DTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
    }
}
