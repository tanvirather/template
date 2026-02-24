using Microsoft.AspNetCore.Mvc;
using Zuhid.Product.Models;

namespace Zuhid.Product.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController(AppSetting appSetting, HttpClient httpClient) : ControllerBase {

  [HttpGet("Json")]
  public async Task<string> Json(string icao = "kden") {
    var result = string.Empty;
    var response = await httpClient.GetAsync($"{appSetting.AviationUrl}?ids={icao}&format=json");
    if (response != null && response.IsSuccessStatusCode) {
      result = await response.Content.ReadAsStringAsync();
    }
    return result;
  }

  [HttpGet("Model")]
  public async Task<TafModel> Model(string icao = "kden") {
    var result = new TafModel();
    var response = await httpClient.GetAsync($"{appSetting.AviationUrl}?ids={icao}&format=json");
    if (response != null && response.IsSuccessStatusCode) {
      var list = await response.Content.ReadFromJsonAsync<List<TafModel>>();
      if (list != null && list.Count > 0) {
        result = list[0];
      }
    }
    return result;
  }
}

