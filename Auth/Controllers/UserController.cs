using Microsoft.AspNetCore.Mvc;

namespace Zuhid.Auth.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller {

  [HttpGet()]
  public string Get() {
    return "User Controller";
  }
}

