using Microsoft.AspNetCore.Mvc;
using Zuhid.Weather.Entities;
using Zuhid.Weather.Repositories;

namespace Zuhid.Weather.Controllers;

[ApiController]
[Route("[controller]")]
public class PostgresController(TafRepository tafRepository) : ControllerBase {

  [HttpGet]
  public async Task<List<TafEntity>> Get() => await tafRepository.Get();
}
