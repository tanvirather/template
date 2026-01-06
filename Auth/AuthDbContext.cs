using Microsoft.EntityFrameworkCore;
using Zuhid.Auth.Entities;
using Zuhid.Base;

namespace Zuhid.Auth;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options) {

  public DbSet<UserEntity> Users { get; set; }
  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);
    builder.ToSnakeCase("auth");
  }
}
