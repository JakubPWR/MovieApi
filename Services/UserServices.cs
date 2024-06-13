using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieApi.Entities;
using MovieApi.Models;

namespace MovieApi.Services
{
    public interface IUserServices
    {
        public int Create(CreateUserDto userDto);
        public string PostRating(int id ,string movie_name,RatingDto RatingDto);
        public IEnumerable<UserDto> GetAllUsers();
        public bool DeleteUser(int id);
        public UserDto GetUser(int id);
    }
    public class UserServices: IUserServices
    {
        private readonly MovieDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserServices(MovieDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _dbContext.Users.Include(u=>u.Ratings).ToList();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return usersDto;
        }
        public UserDto GetUser(int id)
        {
            var user = _dbContext.Users.Include(u=>u.Ratings).FirstOrDefault(u=>u.Id == id);
            var userDto = _mapper.Map<UserDto>(user); 
            return userDto;
        }
        public int Create(CreateUserDto UserDto)
        {
            var user = _mapper.Map<User>(UserDto);
            _dbContext.Add(user);
            _dbContext.SaveChanges();
            return user.Id;
        }
        public string PostRating(int id,string movie_name ,RatingDto RatingDto)
        {
            var postingUser = _dbContext.Users.FirstOrDefault(u=>u.Id == id);
            if(postingUser == default)
            {
                return "User not found";
            }
            var movie = _dbContext.Movies.Include(m=>m.Ratings).FirstOrDefault(m=>m.Name == movie_name);
            if(movie == default)
            {
                return "Movie Not Found";
            }
            var rating = _mapper.Map<Rating>(RatingDto);
            rating.UserId = postingUser.Id;
            movie.Ratings.Add(rating);
            _dbContext.SaveChanges();
            return rating.Id.ToString();
        }
        public bool DeleteUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u=>u.Id == id);
            if(user == default)
            {
                return false;
            }
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            return true;
        }
    }
}