using System.Text.Json;
using Zuhid.Identity.Models;

namespace Zuhid.Identity;

public class AppSetting {
  // private IConfiguration configuration;
  public AppSetting(IConfiguration configuration) {
    // this.configuration = configuration;
    // configuration.GetSection("AppSettings").Bind(this);

    Identity = GetConnectionString(configuration, "Identity");
    Log = configuration.GetConnectionString("Log") ?? "";
    // IdentityModel = configuration.GetValue<IdentityModel>("AppSettings:IdentityModel");
    var corsOrigin = configuration.GetValue<string>("AppSettings:CorsOrigin") ?? "";
    // Console.WriteLine($"AppSetting CorsOrigin: {corsOrigin}");

    configuration.GetSection("AppSettings:IdentityModel").Bind(IdentityModel);
    // Console.WriteLine($"AppSetting IdentityModel: {JsonSerializer.Serialize(IdentityModel)}");

    // Console.WriteLine($"_______________________________________________________________________");
  }

  public string Name { get; set; } = "Auth";
  public string Version { get; set; } = "1.0";
  public string CorsOrigin { get; set; } = "CorsOrigin";
  public string Identity { get; set; } //= GetConnectionString(configuration, "Identity");
  public string Log { get; set; } //= GetConnectionString(configuration, "Log");

  public IdentityModel IdentityModel = new(); //configuration.GetSection("IdentityModel").Get<IdentityModel>() ?? new IdentityModel();

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
