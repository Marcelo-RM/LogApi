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
        Log.Error($"APP: {log.Application} | TITLE: {log.Title} | MESSAGE: {log.Message}");
        break;
      case "WRN":
        Log.Warning($"APP: {log.Application} | TITLE: {log.Title} | MESSAGE: {log.Message}");
        break;
      case "INF":
        Log.Information($"APP: {log.Application} | TITLE: {log.Title} | MESSAGE: {log.Message}");
        break;
      default:
        Log.Fatal($"APP: {log.Application} | TITLE: {log.Title} | MESSAGE: {log.Message}");
        break;
    }

    return Ok("Log salvo!");
  }

}