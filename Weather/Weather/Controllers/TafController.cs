using Microsoft.AspNetCore.Mvc;
using Zuhid.Weather.Repositories;
using Zuhid.Weather.Entities;

namespace Zuhid.Weather.Controllers;

[ApiController]
[Route("[controller]")]
public class TafController(TafRepository tafRepository) : ControllerBase {

  [HttpGet]
  public async Task<List<TafEntity>> Get() => await tafRepository.Get();
}
