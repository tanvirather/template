using Zuhid.Base;

namespace Zuhid.Auth.Entities;

public class UserEntity : BaseEntity {
  public string Email { get; set; } = string.Empty;
  public string HashedPassword { get; set; } = string.Empty;
  public string Secret { get; set; } = string.Empty;
}
