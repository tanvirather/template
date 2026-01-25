using Microsoft.EntityFrameworkCore;
using Zuhid.Auth.Entities;
using Zuhid.Auth.Models;
using Zuhid.Base;

namespace Zuhid.Auth.Repositories;

public class AccountRepository(AuthDbContext context) : BaseRepository<AuthDbContext, AccountModel, UserEntity>(context) {

  protected override IQueryable<AccountModel> Query => _context.Users.Select(entity => new AccountModel {
    Id = entity.Id,
    Email = entity.Email,
    // Passsword = entity.HashedPassword,
  });

  public async Task<UserEntity?> GetEntity(string email) {
    return await _context.Users.Where(n => n.Email.Equals(email.ToLower())).FirstOrDefaultAsync();
  }



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
