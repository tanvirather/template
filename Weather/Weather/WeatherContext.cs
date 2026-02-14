using Microsoft.EntityFrameworkCore;
// using Zuhid.Weather.AviationModels;
using Zuhid.Weather.Entities;

namespace Zuhid.Weather;

public class WeatherContext(DbContextOptions<WeatherContext> options) : DbContext(options) {
  public DbSet<TafEntity> TafEntity { get; set; } = null!;
  // public DbSet<TafModel> TafModel { get; set; } = null!;
}
