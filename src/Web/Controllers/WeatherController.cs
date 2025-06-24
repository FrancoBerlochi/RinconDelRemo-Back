using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("clima")]
    public async Task<IActionResult> GetWeather()
    {
        var weather = await _weatherService.GetWeatherAsync();
        return Ok(weather);
    }

    [HttpGet("week")]
    public async Task<IActionResult> GetWeeklyForecast()
    {
        var forecast = await _weatherService.GetWeeklyForecastAsync();
        return Ok(forecast);
    }
}
