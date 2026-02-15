using Zuhid.Weather.Entities;
using Microsoft.EntityFrameworkCore;

namespace Zuhid.Weather.Repositories;

public class TafRepository(WeatherContext context) {
  public async Task<List<TafEntity>> Get() => await context.TafEntity.ToListAsync();
}

