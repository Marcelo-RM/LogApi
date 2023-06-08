using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((ctx, lc) => lc
  .WriteTo.File("Log/log.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
  .WriteTo.File("Log/log.txt", LogEventLevel.Warning, rollingInterval: RollingInterval.Day)
  .WriteTo.File("Log/log.txt", LogEventLevel.Information, rollingInterval: RollingInterval.Day)
  .WriteTo.File("Log/log.txt", LogEventLevel.Fatal, rollingInterval: RollingInterval.Day));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
