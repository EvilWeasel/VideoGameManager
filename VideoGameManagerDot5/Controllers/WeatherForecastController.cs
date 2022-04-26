using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameManagerDot5.Controllers
{
    /// <summary>
    /// This is the start of our default weatherForecast-Controller. It functions basically just like a router
    /// in ExpressJS
    /// The default name for the route here will get the name from the name of the controller,
    /// where the '...Controller' part is omited automagically
    /// We can also change the route to whatever we want here, for example 'api/weather'
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        /// <summary>
        /// This is the constructor-method of the WeatherForecastController
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// This method defines the code, that will run on the server, whenever someone accesses this
        /// route. This route is configured to return to the caller of the api an object of type 'WeatherForecast',
        /// defined by default in 'WeatherForecast.cs', located in the same namespace.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        /// <summary>
        /// Let's create an additional function, which will respond to POST-requests on the same route,
        /// with a string "Hello World!"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Post()
        {
            return "Hello World!";
        }
    }
}
