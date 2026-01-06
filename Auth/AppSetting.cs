namespace Zuhid.Auth;

public class AppSetting(IConfiguration configuration) {
  public string Name { get; set; } = "Auth";
  public string Version { get; set; } = "1.0";
  public string CorsOrigin { get; set; } = "CorsOrigin";
  public string Auth { get; set; } = GetConnectionString(configuration, "Auth");
  public string Log { get; set; } = GetConnectionString(configuration, "Log");

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
