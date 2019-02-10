using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RatedMoviesDemo.Repository.Entities;

namespace RatedMoviesDemo.Api.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get([FromQuery] string title = null,[FromQuery] uint? year = null,[FromQuery] string[] genres = null)
        {
            throw new NotImplementedException();
        }
    }
}
