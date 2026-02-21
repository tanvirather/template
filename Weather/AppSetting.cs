using Microsoft.Extensions.Configuration;

namespace Zuhid.Weather;

public class AppSetting(IConfiguration configuration) {
  public string Name { get; set; } = configuration.GetValue<string>("AppSetting:Name") ?? "Zuhid.Weather";
  public string Version { get; set; } = configuration.GetValue<string>("AppSetting:Version") ?? "1.0";
  public string[] CorsOrigin { get; set; } = configuration.GetSection("AppSetting:CorsOrigin").Get<string[]>() ?? [];
  public string AviationUrl { get; set; } = configuration.GetValue<string>("AppSetting:AviationUrl") ?? string.Empty;
  public string Weather { get; set; } = GetConnectionString(configuration, "Weather");
  public string Log { get; set; } = GetConnectionString(configuration, "Log");
  public string Weather_Cosmos { get; set; } = GetCosmos(configuration, "Weather_Cosmos");

  /// <summary>
  /// Get Connection string and replace "[postgres_credential]" with value from secrets
  /// </summary>
  /// <param name="configuration"></param>
  /// <param name="connString"></param>
  /// <returns></returns>
  private static string GetConnectionString(IConfiguration configuration, string connString) {
    return (configuration.GetConnectionString(connString) ?? "")
      .Replace("[postgres_credential]", configuration.GetValue<string>("postgres_credential"), StringComparison.Ordinal);
  }
  private static string GetCosmos(IConfiguration configuration, string connString) {
    return (configuration.GetConnectionString(connString) ?? "")
      .Replace("[cosmos_credential]", configuration.GetValue<string>("cosmos_credential"), StringComparison.Ordinal);
  }
}


public class CosmosOptions {
  public string Endpoint { get; init; } = "https://localhost:8081"; // default!;
  public string Key { get; init; } = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="; // default!;
  public string DatabaseId { get; init; } = "Weather";
}

