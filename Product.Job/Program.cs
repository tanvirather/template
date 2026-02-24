// using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
// using Zuhid.Weather.Etls;

namespace Zuhid.Product.Job;

/// <summary>
/// export DOTNET_ENVIRONMENT=Development
/// dotnet run --PostgresEtl
/// dotnet run --CosmosEtl
/// dotnet run --RedisEtl
/// </summary>
public class Program
{
  static async Task Main(string[] args)
  {
    var config = GetConfigurationRoot();
    var appSetting = new AppSetting(config);

    // if (args.Contains("--PostgresEtl"))
    // {
    //   var postgresContext = new PostgresContext(new DbContextOptionsBuilder<PostgresContext>().UseNpgsql(appSetting.Weather).Options);
    //   await new PostgresEtl(appSetting, postgresContext).Run();
    // }
    // if (args.Contains("--CosmosEtl"))
    // {
    //   var cosmosContext = new CosmosContext(new CosmosOptions());
    //   await new CosmosEtl(appSetting, cosmosContext).Run();
    // }
    // if (args.Contains("--RedisEtl"))
    // {
    //   // var redisContext = new RedisContext(new RedisOptions());
    //   await new RedisEtl(appSetting).Run();
    // }
  }

  private static IConfigurationRoot GetConfigurationRoot()
  {
    var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
    var builder = new ConfigurationBuilder()
      .SetBasePath(AppContext.BaseDirectory)
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    if (!string.IsNullOrEmpty(environment))
    {
      builder.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
      // Add user secrets in development environment only
      if (environment.Equals("Development", StringComparison.OrdinalIgnoreCase))
      {
        builder.AddUserSecrets<Program>();
      }
    }
    return builder
      .AddEnvironmentVariables() // allow overrides from env vars
      .Build();
  }
}
