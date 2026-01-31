namespace Zuhid.Identity.Models;

public class LoginResponse {
  /// <summary>
  /// Authentication token
  /// </summary>
  public string Token { get; set; } = "";

  /// <summary>
  /// Is Tfa required
  /// </summary>
  public bool RequireTfa { get; set; } = false;


  /// <summary>
  /// LandingPage
  /// </summary>
  public string LandingPage { get; set; } = "";
}
