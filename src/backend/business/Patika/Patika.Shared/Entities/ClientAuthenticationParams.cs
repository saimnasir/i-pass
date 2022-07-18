namespace Patika.Shared.Entities
{
	public class ClientAuthenticationParams
	{
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string AuthServer { get; set; }
		public ClientAuthenticationParams()
		{

		}
		public ClientAuthenticationParams(Configuration configuration)
		{
			ClientId = configuration.ClientId;
			ClientSecret = configuration.ClientSecret;
			AuthServer = configuration.AuthServerUrl;
		}

		public static implicit operator ClientAuthenticationParams(Configuration c) => new(c);
	}
}