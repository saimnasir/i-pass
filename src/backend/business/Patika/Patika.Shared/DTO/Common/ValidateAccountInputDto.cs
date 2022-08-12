namespace IPass.Shared.DTO.Common
{
    public class ValidateAccountInputDto : Patika.Shared.DTO.DTO
    {
        public string UserName { get; set; }
        public string ActivationCode { get; set; }
    }
}
