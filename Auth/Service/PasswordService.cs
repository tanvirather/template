// https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.passwordhasher-1?view=aspnetcore-10.0

using Microsoft.AspNetCore.Identity;

namespace Zuhid.Auth.Service;

public class PasswordService {
  private readonly PasswordHasher<string> _hasher = new();

  public string Hash(string userName, string password) {
    return _hasher.HashPassword(userName, password);
  }

  public bool Verify(string userName, string hashedPassword, string providedPassword) {
    var result = _hasher.VerifyHashedPassword(userName, hashedPassword, providedPassword);
    return result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
  }
}
