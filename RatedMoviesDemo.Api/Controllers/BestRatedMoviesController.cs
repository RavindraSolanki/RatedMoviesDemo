using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RatedMoviesDemo.Repository.Entities;

namespace RatedMoviesDemo.Api.Controllers
{
    [Route("api/bestratedmovies")]
    [ApiController]
    public class BestRatedMoviesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            throw new NotImplementedException();
        }
    }
}