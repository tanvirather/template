using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zuhid.Identity.Entities;
using Zuhid.Identity.Models;
using Zuhid.Identity.Services;

namespace Zuhid.Identity.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AccountController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, ITokenService tokenService) : ControllerBase {
  [HttpPost("login")]
  public async Task<LoginResponse> Login([FromBody] LoginModel model) {
    var userEntity = await userManager.FindByNameAsync(model.Username);
    if (userEntity != null) {
      var signInResult = await signInManager.CheckPasswordSignInAsync(userEntity, model.Password, false);
      if (signInResult.Succeeded) {
        // var isTfaValid = await userManager.VerifyChangePhoneNumberTokenAsync(userEntity, model.Tfa, userEntity.PhoneNumber);
        // if (signInResult.RequiresTwoFactor && !isTfaValid) {
        //   return new LoginResponse { RequireTfa = true };
        // }
        // var claims = await userManager.GetClaimsAsync(userEntity);
        // var roles = await userManager.GetRolesAsync(userEntity);
        return new LoginResponse {
          Token = tokenService.Build(userEntity.Id, [], []), // claims, roles),
          // LandingPage = userEntity.LandingPage
        };
      }
    }
    throw new ApplicationException("Invalid Login");
  }
}
