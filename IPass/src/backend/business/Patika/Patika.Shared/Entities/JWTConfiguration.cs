namespace Patika.Shared.Entities
{
	public class JWTConfiguration
	{
		public string ValidAudience { get; set; } = string.Empty;
		public string ValidIssuer { get; set; } = string.Empty;
		public string Secret { get; set; } = string.Empty;
		public int TokenValidityInMinutes { get; set; }
		public int RefreshTokenValidityInDays { get; set; }
		public bool ValidateIssuerSigningKey { get; set; } = false;
		public bool ValidateIssuer { get; set; } = false;
		public bool ValidateAudience { get; set; } = false;
		public bool RequireExpirationTime { get; set; } = false;
		public bool ValidateLifetime { get; set; } = false;
		public bool RequireHttpsMetadata { get; set; } = false;
	}
}