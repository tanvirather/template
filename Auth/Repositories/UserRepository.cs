using Microsoft.EntityFrameworkCore;
using Zuhid.Auth.Entities;
using Zuhid.Base;

namespace Zuhid.Auth.Repositories;

public class UserRepository(AuthDbContext context) : BaseRepository<AuthDbContext, UserEntity, UserEntity>(context) {
  protected override IQueryable<UserEntity> Query => _context.Users;

  internal async Task<UserEntity?> SelectByEmail(string email) {
    return await Query.Where(n => n.Email == email).FirstOrDefaultAsync();
  }

  // .Select(entity => new UserModel {
  //   Id = entity.Id,
  //   Email = entity.Email,
  // });

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
