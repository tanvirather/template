
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using Zuhid.Weather.AviationModels;
using Zuhid.Weather.Entities;

namespace Zuhid.Weather.Etls;

public class TafEtl {
  public async Task Run() {
    var modelList = await Extract();
    var entityList = Transform(modelList);
    await Load(entityList);
  }

  private async Task<List<TafModel>> Extract() {
    var client = new HttpClient();
    var response = await client.GetAsync("https://aviationweather.gov/api/data/taf?ids=kden&format=json");
    return response != null && response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<List<TafModel>>() ?? [] : [];
  }
  private List<TafEntity> Transform(List<TafModel> models) {
    return [.. models.Select(m => new TafEntity {
      IcaoId = m.IcaoId,
      Name = m.Name,
      // Fcsts = [.. m.Fcsts.Select(f => new TafForecastEntity {
      //   TimeFrom = f.TimeFrom,
      //   TimeTo = f.TimeTo,
      //   Wdir = f.Wdir,
      //   Wspd = f.Wspd,
      //   Wgst = f.Wgst
      // })]
    })];
  }
  private async Task Load(List<TafEntity> entityList) {
    var context = new WeatherContext(new DbContextOptionsBuilder<WeatherContext>().UseNpgsql("Server=localhost;Database=weather;User Id=postgres;Password=P@ssw0rd;TrustServerCertificate=True;").Options);
    context.Database.EnsureCreated();

    // delete all existing records
    await context.TafEntity.ExecuteDeleteAsync();
    // insert new records
    await context.TafEntity.AddRangeAsync(entityList);
    await context.SaveChangesAsync();
  }
}
