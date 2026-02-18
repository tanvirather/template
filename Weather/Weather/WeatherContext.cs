using Microsoft.EntityFrameworkCore;
using Zuhid.Weather.Entities;

namespace Zuhid.Weather;

public class WeatherContext : DbContext {

  public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) {
    Database.EnsureCreated();
  }

  public DbSet<TafEntity> TafEntity { get; set; } = null!;
}
