using Patika.Shared.Entities;

namespace Patika.Shared.Events
{
    public class ConfigurationEvents
    {
        public delegate void ConfigurationEventArgs(Configuration config);

        public static event ConfigurationEventArgs ConfigurationChanged;
        private static Configuration LatestConfiguration { get; set; }

        public static void NewConfiguration(Configuration configuration)
        {
            LatestConfiguration = configuration;
            ConfigurationChanged?.Invoke(configuration);
        }

        public static Configuration GetConfiguration() => LatestConfiguration;
    }
}