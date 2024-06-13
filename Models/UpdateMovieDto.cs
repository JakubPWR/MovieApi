using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Models
{
    public class UpdateMovieDto
    {
        public string Name { get; set; }
        public DateOnly PremierDate { get; set; }
        public string Genre { get; set; }
    }
}