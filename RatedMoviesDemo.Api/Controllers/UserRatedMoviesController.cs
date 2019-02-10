using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RatedMoviesDemo.Repository.Entities;

namespace RatedMoviesDemo.Api.Controllers
{
    [Route("api/userratedmovies")]
    [ApiController]
    public class UserRatedMoviesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get([FromQuery] int userid)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Post([FromBody] int userId, [FromBody] int movieId, [FromBody] int rating)
        {
            throw new NotImplementedException();
        }
    }
}