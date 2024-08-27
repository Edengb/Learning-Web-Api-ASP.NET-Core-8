using Microsoft.AspNetCore.Mvc;

namespace LoggingAndMiddlewareDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        
        logger.LogDebug("Teste!!");
        logger.LogInformation("This is a logging message with args: Today is {Week}. It is {Time}.", DateTime.Now.DayOfWeek, DateTime.Now.ToLongTimeString());
        logger.LogInformation("This is a logging message with args: Today is {Week}. It is {Time}.", DateTime.Now.DayOfWeek, DateTime.Now.ToLongTimeString());
        logger.LogInformation($"This is a logging message with string concatenation: Today is {DateTime.Now.DayOfWeek}. It is {DateTime.Now.ToLongTimeString()}.");
        
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
