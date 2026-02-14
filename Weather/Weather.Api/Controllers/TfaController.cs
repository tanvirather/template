using Microsoft.AspNetCore.Mvc;

namespace Zuhid.Weather.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TfaController : ControllerBase {

  [HttpGet]
  public string Get() {
    return "Hello World!";
  }
}
