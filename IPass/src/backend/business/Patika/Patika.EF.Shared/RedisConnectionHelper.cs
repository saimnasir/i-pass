using Patika.Shared.Entities;
using Patika.Shared.Events;
using StackExchange.Redis;

namespace Patika.EF.Shared
{
    public class RedisConnectorHelper
    {
        static bool EventListenerCofigured = false;
        private static void SetupListener()
        {
            if (!EventListenerCofigured)
            {
                ConfigurationEvents.ConfigurationChanged += ConfigurationEvents_ConfigurationChanged;
                EventListenerCofigured = true;
            }
            if (LazyConnection == null)
            {
                try
                {
                    LazyConnection = ConnectionMultiplexer.Connect(ConfigurationEvents.GetConfiguration().RedisHost);
                }
                catch
                {
                    LazyConnection = null;
                }
            }
        }

        private static void ConfigurationEvents_ConfigurationChanged(Configuration config)
        {
            LazyConnection = ConnectionMultiplexer.Connect(config.RedisHost);
        }

        private static ConnectionMultiplexer LazyConnection { get; set; } = null;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                SetupListener();
                return LazyConnection;
            }
        }

        public static IDatabase Db => Connection.GetDatabase();
    }
}