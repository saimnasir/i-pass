namespace Patika.IdentityServer.Shared.Dto
{
	public class RefreshTokenInputDto : Patika.Shared.DTO.DTO
	{
		public string? AccessToken { get; set; }
		public string? RefreshToken { get; set; }
	}
}