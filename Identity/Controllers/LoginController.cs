using Microsoft.AspNetCore.Mvc;
using Zuhid.Identity.Models;
using Zuhid.Identity.Services;

namespace Zuhid.Identity.Controllers;

public class LoginController(SecurityService securityService) : ControllerBase {

  [HttpPost("login")]
  public async Task<LoginResponse> Login(string username, string password) {
    var user = await securityService.AuthenticateUserAsync(username, password);
    return new LoginResponse { Token = $"{user?.UserName}.{user?.Id}" };
  }
}

