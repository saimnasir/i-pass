using Patika.AuthenticationServer;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var builder = WebApplication.CreateBuilder(args);

builder.Configuration
	.AddJsonFile($"appsettings.json", true)
	.AddJsonFile($"appsettings.{env}.json", true)
	.AddEnvironmentVariables();

var startup = new Startup(builder.Configuration, builder.Environment);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app);

app.Run();