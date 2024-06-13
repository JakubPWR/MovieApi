using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApi.Entities;

namespace MovieApi.Models
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly PremierDate { get; set; }
        public double MeanRating { get; set; }
        public string GenreString { get; set; }
        public List<RatingDto> Ratings { get; set; }
    }
}