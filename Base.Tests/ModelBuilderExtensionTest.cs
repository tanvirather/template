using Microsoft.EntityFrameworkCore;

namespace Zuhid.Base.Tests;

public class ModelBuilderExtensionTest {
  private class TestDbContext : DbContext {
    public DbSet<UserProfile> UserProfiles { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseInMemoryDatabase("TestDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.ToSnakeCase();
    }
  }

  private class UserProfile {
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
  }

  [Fact]
  public void ToSnakeCase_ConvertsEntityAndPropertyNames() {
    using var context = new TestDbContext();
    var model = context.Model;

    var entity = model.FindEntityType(typeof(UserProfile))!;
    Assert.Equal("user_profile", entity.GetTableName());

    var property = entity.GetProperties().First(p => p.Name == "Id");
    Assert.Equal("id", property.GetColumnName());

    property = entity.GetProperties().First(p => p.Name == "FirstName");
    Assert.Equal("first_name", property.GetColumnName());
  }
}
