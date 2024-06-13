using AutoMapper;
using MovieApi.Entities;
using MovieApi.Models;

namespace MovieApi
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.GenreString, opt => opt.MapFrom(src => src.Genre.ToString()));
            CreateMap<User, UserDto>();
            CreateMap<Rating, RatingDto>();
            CreateMap<RatingDto, Rating>();
            CreateMap<CreateUserDto, User>();
            CreateMap<CreateMovieDto, Movie>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom<GenreResolver>());
            CreateMap<UpdateMovieDto, Movie>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom<GenreResolver2>());
        }
    }
}
