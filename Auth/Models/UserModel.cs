using Zuhid.Base;

namespace Zuhid.Auth.Models;

public class UserModel : BaseModel {
  public string Email { get; set; } = string.Empty;
}
