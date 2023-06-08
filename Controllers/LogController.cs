using Microsoft.AspNetCore.Mvc;
using Serilog;
using LogApi.Models;

namespace LogApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LogController : ControllerBase
{
  [HttpPost(Name = "SetLog")]
  public ObjectResult SetLog(LogData log)
  {

    switch (log.Level)
    {
      case "ERR":
        Log.Error($"{log.Application} | {log.Title} - {log.Message}");
        break;
      case "WRN":
        Log.Warning($"{log.Application} | {log.Title} - {log.Message}");
        break;
      case "INF":
        Log.Information($"{log.Application} | {log.Title} - {log.Message}");
        break;
      default:
        Log.Fatal($"{log.Application} | {log.Title} - {log.Message}");
        break;
    }

    return Ok("Log salvo!");
  }

}
