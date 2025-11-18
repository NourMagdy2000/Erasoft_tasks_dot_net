using MoviesApp.Models;

namespace MoviesApp.ViewModels
{
    public class UpdateCinemaVM
    {
        public Cinema Cinema { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public List<int> CinemaMovies { get; set; }
    }
}
