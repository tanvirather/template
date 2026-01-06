using Zuhid.Auth.Entities;
using Zuhid.Auth.Models;
using Zuhid.Base;

namespace Zuhid.Auth.Repositories;

public class UserRepository(AuthDbContext context) : BaseRepository<AuthDbContext, UserModel, UserEntity>(context) {
  protected override IQueryable<UserModel> Query => _context.Users.Select(entity => new UserModel {
    Id = entity.Id,
    Email = entity.Email,
  });

  // public async Task<List<UserEntity>> Get() {
  //   return await authDbContext.Users.ToListAsync();
  // }

  // protected override IQueryable<Models.User> Query => _context.Users.Select(entity => new Models.User {
  //   Id = entity.Id,
  //   UpdatedById = entity.UpdatedById,
  //   UpdatedDateTime = entity.UpdatedDateTime,
  //   UserName = entity.UserName ?? string.Empty,
  //   Email = entity.Email ?? string.Empty,
  //   PhoneNumber = entity.PhoneNumber ?? string.Empty,
  //   ConcurrencyStamp = entity.ConcurrencyStamp ?? string.Empty,
  // });
}
