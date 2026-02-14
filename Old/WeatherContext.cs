using Microsoft.EntityFrameworkCore;
using Zuhid.Base;

namespace Zuhid.Weather;

public class WeatherContext(DbContextOptions<WeatherContext> options) : DbContext(options) {
  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);
    builder.ToSnakeCase("weather");
  }
}
