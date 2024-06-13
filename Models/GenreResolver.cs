using AutoMapper;
using MovieApi.Entities;

namespace MovieApi.Models
{
    public class GenreResolver : IValueResolver<CreateMovieDto, Movie, Genre>
    {
        public Genre Resolve(CreateMovieDto source, Movie destination, Genre destMember, ResolutionContext context)
        {
            if (Enum.TryParse(source.Genre, true, out Genre genre))
            {
                return genre;
            }
            else
            {
                // Handle invalid genre here, you can throw an exception or return a default value
                return Genre.Diffrent; // Example default value
            }
        }
    }
}
