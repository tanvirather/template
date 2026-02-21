using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Zuhid.Weather.Controllers;

[ApiController]
[Route("[controller]")]
public class RedisController() : ControllerBase {

  [HttpGet()]
  public async Task<string> Get() {
    var connection = ConnectionMultiplexer.Connect("localhost:6379");
    var database = connection.GetDatabase();
    var tafData = await database.StringGetAsync("taf_data");
    return tafData.ToString();
  }
}
