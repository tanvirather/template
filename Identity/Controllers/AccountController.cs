using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zuhid.Identity.Models;
using Zuhid.Identity.Services;

namespace Zuhid.Identity.Controllers;


[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AccountController(SecurityService securityService) : ControllerBase {
  [HttpPost("login")]
  public async Task<LoginResponse> Login([FromBody] LoginModel model) {
    var user = await securityService.AuthenticateUserAsync(model.Username, model.Password);
    return new LoginResponse { Token = $"{user?.Id}.{user?.UserName}" };
  }
}


public class LoginModel {
  public string? Username { get; set; }
  public string? Password { get; set; }
}
