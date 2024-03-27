using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestInitProject.Application.WeatherForecasts.Queries.GetWeatherForecasts;


[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] summaries =
    [
        "Freezi", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;

    public WeatherForecastController(IConfiguration configuration, IMediator mediator)
    {
        _configuration = configuration;
        _mediator = mediator;
    }

    [HttpGet("")]
    [AllowAnonymous]
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
    {
        return await _mediator.Send(new GetWeatherForecastsQuery());
    }

    [HttpGet("connectionstring")]
    [AllowAnonymous]
    public IActionResult GetConnectionString()
    {
        Console.WriteLine("I'm in here");
        // throw new Exception("Something went wrong");
        return Ok(_configuration.GetConnectionString("DefaultConnection"));
    }
}

