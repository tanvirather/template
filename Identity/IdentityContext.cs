using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zuhid.Base;
using Zuhid.Identity.Entities;

namespace Zuhid.Identity;

public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<UserEntity, RoleEntity, Guid,
  UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options) {
  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
    base.ConfigureConventions(configurationBuilder);
    configurationBuilder.Properties<DateTime>().HaveColumnType("timestamp without time zone");
  }

  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);
    builder.Entity<UserEntity>(entity => entity.ToTable("Users"));
    builder.Entity<RoleEntity>(entity => entity.ToTable("Role"));
    builder.Entity<UserClaim>(entity => entity.ToTable("UserClaim"));
    builder.Entity<UserRole>(entity => entity.ToTable("UserRole"));
    builder.Entity<UserLogin>(entity => entity.ToTable("UserLogin"));
    builder.Entity<RoleClaim>(entity => entity.ToTable("RoleClaim"));
    builder.Entity<UserToken>(entity => entity.ToTable("UserToken"));
    builder.ToSnakeCase("identity");

    // Data Seeding
    builder.LoadCsvData<UserEntity>();
    builder.LoadCsvData<RoleEntity>();
    builder.LoadCsvData<UserRole>();
    builder.LoadCsvData<UserClaim>();
  }
}
