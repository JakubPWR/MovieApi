using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Models
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public DateOnly PublicationDate { get; set; }
        public int UserId { get; set; }
    }
}