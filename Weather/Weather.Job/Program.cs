using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Zuhid.Weather.Etls;
// using Zuhid.Weather.Etls;

namespace Zuhid.Weather.Job;

/// <summary>
/// export DOTNET_ENVIRONMENT=Development
/// dotnet run --TafToPostgresEtl
/// </summary>
public class Program {
  static async Task Main(string[] args) {
    var config = GetConfigurationRoot();
    var appSetting = new AppSetting(config);
    var weatherContext = new WeatherContext(new DbContextOptionsBuilder<WeatherContext>().UseNpgsql(appSetting.Weather).Options);

    if (args.Contains("--TafToPostgresEtl")) {
      Console.WriteLine("Running TafToPostgresEtl...");
      await new TafToPostgresEtl(appSetting, weatherContext).Run();
    }
    if (args.Contains("--metar")) {
      Console.WriteLine("Running MetarEtl...");
      // await new MetarEtl().Run();
    }
  }

  private static IConfigurationRoot GetConfigurationRoot() {
    var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
    var builder = new ConfigurationBuilder()
      .SetBasePath(AppContext.BaseDirectory)
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    if (!string.IsNullOrEmpty(environment)) {
      builder.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
      // Add user secrets in development environment only
      if (environment.Equals("Development", StringComparison.OrdinalIgnoreCase)) {
        builder.AddUserSecrets<Program>();
      }
    }
    var config = builder
      .AddEnvironmentVariables() // allow overrides from env vars
      .Build();
    return config;
  }
}
