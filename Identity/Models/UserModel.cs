using Zuhid.Base;

namespace Zuhid.Identity.Models;

public class UserModel : BaseModel {
  public string Email { get; set; } = string.Empty;
}
