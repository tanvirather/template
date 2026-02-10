using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zuhid.Base;
using Zuhid.Identity.Entities;
using Zuhid.Identity.Mappers;
using Zuhid.Identity.Models;
using Zuhid.Identity.Repositories;

namespace Zuhid.Identity.Controllers;

public class UserController(UserRepository repository, UserMapper mapper, UserManager<UserEntity> userManager) :
  BaseCrudController<UserRepository, UserMapper, IdentityContext, UserModel, UserEntity>(repository, mapper) {

  // [HttpPost]
  // public override async Task Add([FromBody] UserModel model) {
  //   var email = ("test" + Guid.NewGuid() + "@gmail.com").Trim().ToLower();
  //   var userEntity = new UserEntity {
  //     Id = Guid.NewGuid(),
  //     UserName = email,
  //     Email = email,
  //     UpdatedDateTime = DateTimeOffset.UtcNow.DateTime,
  //   };
  //   var identityResult = await userManager.CreateAsync(userEntity).ConfigureAwait(false);
  // }


  [HttpPost]
  public override async Task Add([FromBody] UserModel model) {
    var userEntity = await userManager.FindByNameAsync("test@gmail.com").ConfigureAwait(false);
    // await userManager.SetPhoneNumberAsync(userEntity, "1234567890").ConfigureAwait(false);
    await userManager.AddToRolesAsync(userEntity, ["User Administrator"]).ConfigureAwait(false);
  }

  [HttpPut]
  public override async Task Update([FromBody] UserModel model) {
    var userEntity = await userManager.FindByNameAsync("test@gmail.com").ConfigureAwait(false);
    await userManager.SetPhoneNumberAsync(userEntity, "1234567890").ConfigureAwait(false);
  }
}
