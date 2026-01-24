
using Microsoft.AspNetCore.Mvc;
using Zuhid.Auth.Models;
using Zuhid.Auth.Repositories;

namespace Zuhid.Auth.Controllers;

public class AccountController(UserRepository repository) : ControllerBase {
  [HttpPost("Login")]
  public virtual async Task<string> Login([FromBody] AccountModel model) {
    var userEntity = await repository.SelectByEmail(model.Email).ConfigureAwait(false);
    if (userEntity != null) {
      if (model.Password.Equals(userEntity.HashedPassword, StringComparison.Ordinal)) {
        return "Success";
      }
    }
    return string.Empty;
  }
}
