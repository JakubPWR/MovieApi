using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public DateOnly PublicationDate { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie {get; set; }
    }
}