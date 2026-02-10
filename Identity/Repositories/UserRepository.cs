using Microsoft.AspNetCore.Identity;
using Zuhid.Base;
using Zuhid.Identity.Entities;
using Zuhid.Identity.Models;

namespace Zuhid.Identity.Repositories;

public class UserRepository(IdentityContext context, UserManager<UserEntity> userManager) : BaseRepository<IdentityContext, UserModel, UserEntity>(context) {

  protected override IQueryable<UserModel> Query => _context.Users.Select(entity => new UserModel {
    Id = entity.Id,
    Email = entity.Email ?? string.Empty,
  });

  public async Task Add(UserEntity entity, string[] roles) {
    await using var txn = await _context.Database.BeginTransactionAsync();
    var userResult = await userManager.CreateAsync(entity).ConfigureAwait(false);
    if (!userResult.Succeeded) {
      await txn.RollbackAsync();
    }
    if (userResult.Succeeded && roles != null && roles.Length > 0) {
      var roleResult = await userManager.AddToRolesAsync(entity, roles).ConfigureAwait(false);
      if (!roleResult.Succeeded) {
        await txn.RollbackAsync();
      }
    }
    await txn.CommitAsync();
  }
}
