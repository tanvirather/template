namespace Zuhid.Product;

public class AppSetting {
  public AppSetting(IConfiguration configuration) {
    configuration.GetSection("AppSettings").Bind(this);
    Product = GetConnectionString(configuration, "Product");
    Log = GetConnectionString(configuration, "Log");
  }
  public string Name { get; set; } = default!;
  public string Version { get; set; } = default!;
  public string CorsOrigin { get; set; } = default!;
  public string AviationUrl { get; set; } = default!;
  public string Product { get; set; } = default!;
  public string Log { get; set; } = default!;

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
}
