using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly PremierDate { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        public double MeanRating { get; set; }
        public Genre Genre { get; set; }
    }
}