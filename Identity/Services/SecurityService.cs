using Microsoft.AspNetCore.Identity;
using Zuhid.Identity.Entities;

namespace Zuhid.Identity.Services;

public class SecurityService(UserManager<UserEntity> userManager) {
  public async Task<UserEntity?> AuthenticateUserAsync(string username, string password) {
    var user = await userManager.FindByNameAsync(username);
    return user != null && await userManager.CheckPasswordAsync(user, password) ? user : null;
  }
}
