using Microsoft.EntityFrameworkCore;
using Zuhid.Base;
using Zuhid.Product.Entities;

namespace Zuhid.Product;

public class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options) {

  public DbSet<PostgresTypeEntity> PostgresTypes { get; set; }
  public DbSet<TafEntity> TafEntity { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);
    builder.ToSnakeCase("product");
  }
}
