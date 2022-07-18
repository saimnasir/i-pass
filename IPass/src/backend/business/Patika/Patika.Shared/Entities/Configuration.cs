using Patika.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Patika.Shared.Entities
{
    public class Configuration : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string RedisHost { get; set; } = "";
        public string RabbitMQHostName { get; set; } = ""; 
        public string GatewayUrl { get; set; } = "";
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string AuthServerUrl { get; set; } = string.Empty;
        public string ApplicationName { get; set; } = string.Empty;
        public bool AutoMigrate { get; set; } = false;
        public List<RDBConnectionString> RDBMSConnectionStrings { get; set; } = new List<RDBConnectionString>();
        public IDictionary<string, string> NoSqlConnectionString { get; set; } = new Dictionary<string, string>();
        public AzureConfig AzureConfiguration { get; set; }
        public HangfireConfig HangfireConfig { get; set; }
        public AccountConfig AccountConfig { get; set; }
		public JWTConfiguration JWT { get; set; }
        public bool AcquireToken { get; set; } = false;
	}

}