using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace c19_38_BackEnd.Controllers
{
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
            _logger = logger;
        }
       
        /// <summary>
        /// Obtiene una lista de pronósticos meteorológicos para los próximos 5 días.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de pronósticos meteorológicos basados en el día actual más los próximos 5 días.
        /// Cada pronóstico incluye la fecha, la temperatura en grados Celsius y un resumen descriptivo del clima.
        /// </remarks>

        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>),200)]
        [ProducesResponseType(500)]
        [HttpGet("GetWeatherForecast")]
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
}
