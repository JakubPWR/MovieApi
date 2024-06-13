using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [Route("api/movie")]
    public class MovieController: ControllerBase
    {
        private readonly IMovieServices _services;
        public MovieController(IMovieServices services)
        {
            _services = services;
        }
        [HttpGet]
        public ActionResult<IEnumerable<MovieDto>> GetAll()
        {
            var movies = _services.GetAll();
            return Ok(movies);
        }
        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            var movie = _services.GetById(id);
            if(movie == default)
            {
                return NotFound();
            }
            return Ok(movie);
        }
        [HttpGet("mean/{movie_name}")]
        public ActionResult DisplayMeanRating(string movie_name)
        {
            var mean = _services.DisplayMeanRating(movie_name);
            if(mean == -1.0)
            {
                return NotFound("Movie Not Found");
            }
            return Ok(mean);
        }
        [HttpPost("post")]
        public ActionResult Create([FromBody] CreateMovieDto movieDto) 
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var movie = _services.Create(movieDto);
            return Ok(movie);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var movie = _services.Delete(id);
            if(movie == false)
            {
                return NotFound("Movie not found");
            }
            return Ok($"Movie with id {id} was deleted");
        }
        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] UpdateMovieDto movieDto)
        {
            var movie = _services.Put(id,movieDto);
            if(movie == false)
            {
                return NotFound("Movie not found");
            }
            return Ok("Movie Updated");
        }
    }
}