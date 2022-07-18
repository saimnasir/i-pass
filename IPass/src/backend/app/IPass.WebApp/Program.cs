using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Serilog;

namespace IPass.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SetupLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    webBuilder.ConfigureAppConfiguration(cfg => {
                        cfg.AddJsonFile($"appsettings.json", true);
                        cfg.AddJsonFile($"appsettings.{env}.json", true);
                        cfg.AddEnvironmentVariables();
                    });
                    webBuilder.UseStartup<Startup>();
                });

        private static void SetupLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("appName", "WebApp")
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}