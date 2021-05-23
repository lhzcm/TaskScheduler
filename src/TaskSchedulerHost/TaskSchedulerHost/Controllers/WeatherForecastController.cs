using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskSchedulerModel.Models;
using TaskSchedulerRespository.DbContexts;

namespace TaskSchedulerHost.Controllers
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
        private TaskSchedulerDbContext _db;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, TaskSchedulerDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IEnumerable<TaskInfo> Get()
        {
            var rng = new Random();
           
            return _db.TaskInfos.ToList();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)],
            //})
            //.ToArray();
        }
    }
}
