using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApi.Entities;

namespace MovieApi
{
    public class MovieSeeder
    {
        private readonly MovieDbContext _dbContext;
        public MovieSeeder(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Movies.Any())
                {
                    var movies = GetMovies();
                    _dbContext.Movies.AddRange(movies);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Movie> GetMovies()
        {
            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Name = "The Lobster",
                    Genre = Genre.Diffrent,
                },
                new Movie()
                {
                    Name = "The Gentlemen",
                    Genre = Genre.Action,
                },
                new Movie()
                {
                    Name = "Avengers",
                    Genre = Genre.Action,
                }

            };
            return movies;
        }
    }
}