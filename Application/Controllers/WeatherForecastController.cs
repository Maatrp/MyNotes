using Microsoft.AspNetCore.Mvc;
using MyNotes.Application.Database;
using MyNotes.Application.Services;
using MyNotes.Domain.Entities;
using System.Globalization;

namespace MyNotes.Application.Controllers
{
    [ApiController]
    [Route("api/WeatherForecast")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly INotesService _notesService;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
            INotesService notesService,
            IConfiguration configuration)
        {
            _logger = logger;
            _notesService = notesService;
            _configuration = configuration;
        }

        [HttpGet("Empty")]
        public IEnumerable<WeatherForecast> Get()
        {
            float f = 353.4535f;
            if (_configuration.GetValue<bool>("UseCommaInsteadOfPointInFloats"))
            {
                _logger.LogWarning(f.ToString(CultureInfo.GetCultureInfo("es-ES")));

            }
            else
            {
                _logger.LogWarning(f.ToString(CultureInfo.GetCultureInfo("en-GB")));
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> GetWithNumber(int numberofrandom)
        {
            _logger.LogInformation($"GetWithNumber started with parameter {numberofrandom}");
            if (numberofrandom < 0)
            {
                _logger.LogDebug("Se ha introducido un número inválido");
            }
            return Enumerable.Range(1, numberofrandom).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("PricePvp")]
        public NotesResponse GetPricePvp()
        {
            NotesResponse r = new NotesResponse();
            Note p = _notesService.GetPricePvp();
            if(p != null)
            {
                r.Price = p;
                r.IsSuccess = true;
            }
            return r;
        }


        [HttpPost("Post")]
        public IEnumerable<WeatherForecast> Post(int numberofrandom)
        {

            return Enumerable.Range(1, numberofrandom).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

    }
}
