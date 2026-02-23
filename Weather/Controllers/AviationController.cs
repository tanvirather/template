using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using Zuhid.Weather.AviationModels;

namespace Zuhid.Weather.Controllers;

[ApiController]
[Route("[controller]")]
public class AviationController(AppSetting appSetting, HttpClient httpClient) : ControllerBase {

  [HttpGet("String")]
  public async Task<string> String() {
    var result = string.Empty;
    var response = await httpClient.GetAsync($"{appSetting.AviationUrl}?ids=kden&format=json");
    if (response != null && response.IsSuccessStatusCode) {
      result = await response.Content.ReadAsStringAsync();
    }
    return result;
  }

  [HttpGet("Model")]
  public async Task<TafModel> Model(string icao) {
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
