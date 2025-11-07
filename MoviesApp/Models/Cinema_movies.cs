using Microsoft.EntityFrameworkCore;

namespace MoviesApp.Models
{
    [PrimaryKey (nameof(MovieId), nameof(CinemaId))]
    public class Cinema_movies
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int  CinemaId{ get; set; }
        public Cinema Cinema { get; set; }
    }
}
