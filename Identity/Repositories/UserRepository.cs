using Zuhid.Base;
using Zuhid.Identity.Entities;
using Zuhid.Identity.Models;

namespace Zuhid.Identity.Repositories;

public class UserRepository(IdentityContext context) : BaseRepository<IdentityContext, UserModel, UserEntity>(context) {

  protected override IQueryable<UserModel> Query => _context.Users.Select(entity => new UserModel {
    Id = entity.Id,
    Email = entity.Email ?? string.Empty,
  });
}
