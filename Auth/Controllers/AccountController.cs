using Microsoft.AspNetCore.Mvc;
using Zuhid.Auth.Entities;
using Zuhid.Auth.Mappers;
using Zuhid.Auth.Models;
using Zuhid.Auth.Repositories;
using Zuhid.Auth.Tools;
using Zuhid.Base;

namespace Zuhid.Auth.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(AccountRepository repository, AccountMapper mapper) :
  BaseCrudController<AccountRepository, AccountMapper, AuthDbContext, AccountModel, UserEntity>(repository, mapper) {

  [HttpPost("Register")]
  public async Task Register([FromBody] AccountModel model) {
    var entity = mapper.GetEntity(model);
    entity.Secret = SecurityTool.GenerateBase32Secret(50);
    await repository.Add(entity).ConfigureAwait(false);
  }

  [HttpPost("Login")]
  public async Task<bool> Login([FromBody] AccountModel model) {
    var entity = await repository.GetEntity(model.Email);
    if (entity != null) {
      var isPasswordValid = SecurityTool.VerifyPassword(model.Password, entity.HashedPassword);
      return isPasswordValid;
    }
    return false;
  }

  [HttpGet("Protect")]
  public string Protect() {
    return SecurityTool.Protect();
  }


  [HttpGet("VerifyProtect")]
  public string VerifyProtect(string token) {
    return SecurityTool.VerifyProtect(token);
  }


  [HttpGet("Totp")]
  public string Totp() {
    // var userEntity = await repository.GetEntity(email);
    return SecurityTool.GenerateTotp(SecurityTool.GenerateBase32Secret(8), 100, 8);
  }


  [HttpGet("QrCode")]
  public async Task<IActionResult?> QrCode(string email) {
    var userEntity = await repository.GetEntity(email);
    var qrBytes = SecurityTool.GenerateQrCode("Zuhid.Auth", userEntity!.Email, userEntity!.Secret);
    return File(qrBytes, "image/png");
  }
}
