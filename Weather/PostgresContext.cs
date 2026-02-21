using Microsoft.EntityFrameworkCore;
using Zuhid.Weather.Entities;

namespace Zuhid.Weather;

public class PostgresContext : DbContext {

  public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) {
    Database.EnsureCreated();
  }

  public DbSet<TafEntity> TafEntity { get; set; } = null!;
}
