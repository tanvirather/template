using Microsoft.EntityFrameworkCore;
using Zuhid.Weather.Entities;

namespace Zuhid.Weather.Repositories;

public class TafRepository(WeatherContext context) {
  public async Task<List<TafEntity>> Get() => await context.TafEntity.ToListAsync();
}

