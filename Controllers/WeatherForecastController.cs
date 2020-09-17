using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace autofac_sample.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class WeatherForecastController : ControllerBase {

        public IDbService DbService { private get; set; }

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController (
            ILogger<WeatherForecastController> logger,
            IDbService dbService
        ) {
            _logger = logger;

        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get () {
            _logger.LogInformation ($"IDbService: {this.DbService.GetHashCode()}");
            this.DbService.Say();

            var rng = new Random ();
            return Enumerable.Range (1, 5).Select (index => new WeatherForecast {
                    Date = DateTime.Now.AddDays (index),
                        TemperatureC = rng.Next (-20, 55),

                })
                .ToArray ();
        }
    }
}