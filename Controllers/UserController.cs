using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/user")]
    public class UserController: ControllerBase
    {
        private readonly IUserServices _services;
        public UserController(IUserServices services)
        {
            _services = services;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = _services.GetAllUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUser([FromRoute] int id)
        {
            var user = _services.GetUser(id);
            return Ok(user);
        }
        [HttpPost("create")]
        public ActionResult CreateUser([FromBody] CreateUserDto UserDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _services.Create(UserDto);
            return Ok(id);
        }
        [HttpPost("rate/{id}/{movie_name}")]

        public ActionResult PostRating([FromRoute] int id,[FromRoute] string movie_name,
        [FromBody] RatingDto ratingDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var rating = _services.PostRating(id ,movie_name,ratingDto);
            return Ok(rating);
        }
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteUser([FromRoute]int id)
        {
            var user = _services.DeleteUser(id);
            if(user == true)
            {
                return Ok(id);
            }
            else
            {
                return NotFound("User not found");
            }
        }
    }
}