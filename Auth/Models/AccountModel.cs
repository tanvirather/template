using Zuhid.Base;

namespace Zuhid.Auth.Models;

public class AccountModel : BaseModel {
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string Totp { get; set; } = string.Empty;
}

