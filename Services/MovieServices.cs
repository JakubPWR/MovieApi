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
    public interface IMovieServices
    {
        public IEnumerable<MovieDto> GetAll();
        public MovieDto GetById(int id);
        public double DisplayMeanRating(string movie_name);
        public int Create(CreateMovieDto movieDto);
        public bool Delete(int id);
        public bool Put(int id, UpdateMovieDto movieDto);
    }
    public class MovieServices: IMovieServices
    {
        private readonly MovieDbContext _dbContext;
        private readonly IMapper _mapper;
        public MovieServices(MovieDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<MovieDto> GetAll()
        {
            var movies = _dbContext.Movies.Include(m=>m.Ratings).ToList();
            var moviesDto = _mapper.Map<List<MovieDto>>(movies);

            return moviesDto;
        }
        public MovieDto GetById(int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(m=>m.Id == id);
            var movieDto = _mapper.Map<MovieDto>(movie);
            return movieDto;
        }
        public double DisplayMeanRating(string movie_name)
        {
            var movie = _dbContext.Movies.Include(m=>m.Ratings)
            .FirstOrDefault(m=>m.Name == movie_name);
            if(movie == default)
            {
                return -1.0;
            }
            var ratings = movie.Ratings;
            int sum = 0;
            foreach(var rating in ratings)
            {
                sum += rating.Score;
            }
            double mean = sum/ratings.Count;
            movie.MeanRating = mean;
            _dbContext.SaveChanges();
            return mean;
        }
        public int Create(CreateMovieDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            _dbContext.Add(movie);
            _dbContext.SaveChanges();
            return movie.Id;
        }
        public bool Delete(int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(m=>m.Id == id);
            if(movie == default)
            {
                return false;
            }
            _dbContext.Remove(movie);
            _dbContext.SaveChanges();
            return true;
        }
        public bool Put(int id, UpdateMovieDto movieDto)
        {
            var movie = _dbContext.Movies.FirstOrDefault(m=>m.Id == id);
            if(movie == default)
            {
                return false;
            }
            var movie_update = _mapper.Map<Movie>(movieDto);
            foreach(var prop in movie.GetType().GetProperties())
            {
                if(prop.GetValue(movie_update) == null || prop.Name == "Id")
                {
                    continue;
                }
                prop.SetValue(movie,prop.GetValue(movie_update));
            }
            _dbContext.SaveChanges();
            return true;
        }
    }
}