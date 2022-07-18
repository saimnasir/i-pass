namespace IPass.Shared.DTO.Common
{
    public class ValidateActivationCodeInputDto : Patika.Shared.DTO.DTO
    {
		public string PhoneNumber { get; set; }
		public string ActivationCode { get; set; }
    }
}