using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Zuhid.Weather.Controllers;

[ApiController]
[Route("[controller]")]
public class CosmosController(CosmosContext cosmosContext) : ControllerBase {

  [HttpGet()]
  public async Task<string> Get() {
    var item = await cosmosContext.Tafs.ReadItemAsync<dynamic>("KDEN", new PartitionKey("KDEN"));
    return item.Resource.ToString();
  }
}
