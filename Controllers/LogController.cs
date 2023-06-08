using Microsoft.AspNetCore.Mvc;

namespace LogApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LogController : ControllerBase
{
  [HttpGet(Name = "GetLog")]
  public ObjectResult Get()
  {
    return Ok("Tudo funcionando!");
  }

}
