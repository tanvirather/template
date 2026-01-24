using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zuhid.Base;
using Zuhid.Identity.Entities;

namespace Zuhid.Identity;

public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<UserEntity, RoleEntity, Guid>(options) {
  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);
    builder.Entity<UserEntity>(entity => entity.ToTable("User"));
    builder.Entity<RoleEntity>(entity => entity.ToTable("Role"));
    builder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable("UserClaim"));
    builder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable("UserRole"));
    builder.Entity<IdentityUserLogin<Guid>>(entity => entity.ToTable("UserLogin"));
    builder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable("RoleClaim"));
    builder.Entity<IdentityUserToken<Guid>>(entity => entity.ToTable("UserToken"));

    // Data Seeding
    builder.LoadCsvData<UserEntity>();
  }
}
