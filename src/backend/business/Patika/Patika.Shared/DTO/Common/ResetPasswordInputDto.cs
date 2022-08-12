namespace IPass.Shared.DTO.Common
{
	public class ResetPasswordInputDto : Patika.Shared.DTO.DTO
    {
		public string UserName { get; set; } = string.Empty;
		public string ActivationCode { get; set; } = string.Empty;
		public string NewPassword { get; set; } = string.Empty;
	}
}