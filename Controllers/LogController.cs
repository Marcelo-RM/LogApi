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

	[HttpGet(Name = "GetLog")]
	public ObjectResult GetLog()
	{
		List<LogData> logs = new List<LogData>();
		string path = $@"Log/log{DateTime.Now.ToString("yyyyMMdd")}.txt";

		using (FileStream stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			using (StreamReader reader = new StreamReader(stream))
			{
				string line = reader.ReadLine();
				while (line != null)
				{
					LogData log = new LogData();

					try
					{
						string level = line.Split('[', ']')[1];
						log.Level = level;

						int startAppIndex = line.IndexOf("APP:") + 4;
						int endAppIndex = line.IndexOf("|", startAppIndex);
						string application = line.Substring(startAppIndex, (endAppIndex - startAppIndex)).Trim();
						log.Application = application;

						int startTitleIndex = line.IndexOf("TITLE:") + 6;
						int endTitleIndex = line.IndexOf("|", startTitleIndex);
						string title = line.Substring(startTitleIndex, (endTitleIndex - startTitleIndex)).Trim();
						log.Title = title;

						int startMsgIndex = line.IndexOf("MESSAGE:") + 8;
						string message = line.Substring(startMsgIndex).Trim();
						log.Message = message;

						string dateTime = line.Substring(0, 30);
						log.DateTime = dateTime;
					}
					catch (Exception)
					{
						log.Message = line;
					}

					logs.Add(log);
					line = reader.ReadLine();
				}
			}
		}

		return Ok(logs);
	}

}