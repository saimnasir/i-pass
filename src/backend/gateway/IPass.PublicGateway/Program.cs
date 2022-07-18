using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace IPass.PublicGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    webBuilder.ConfigureAppConfiguration(cfg => {
                        cfg.AddJsonFile($"ocelot.json", true);
                        cfg.AddJsonFile($"appsettings.json", true);
                        cfg.AddJsonFile($"appsettings.{env}.json", true);
                        cfg.AddJsonFile($"ocelot.{env}.json", true);
                        cfg.AddEnvironmentVariables();
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}