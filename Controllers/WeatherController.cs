using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigiPayTest.Interfaces.Repositories;
using DigiPayTest.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigiPayTest.Controllers
{
    [Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeather _wetherepository;

        public WeatherController(IWeather authRepository)
        {
            _wetherepository = authRepository ??
                throw new ArgumentNullException(nameof(authRepository));

        }
        [HttpGet]
        [Route("GetData")]
        public async Task<ActionResult<string>> GetData()
        {

            var insertWeather = await _wetherepository.GetDataApi();
            if (insertWeather == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }


    }

