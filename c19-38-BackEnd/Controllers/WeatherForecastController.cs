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
        /// Obtiene una lista de pron�sticos meteorol�gicos para los pr�ximos 5 d�as.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de pron�sticos meteorol�gicos basados en el d�a actual m�s los pr�ximos 5 d�as.
        /// Cada pron�stico incluye la fecha, la temperatura en grados Celsius y un resumen descriptivo del clima.
        /// </remarks>

        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>),200)]
        [ProducesResponseType(500)]
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
}
