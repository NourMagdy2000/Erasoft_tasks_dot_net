using MoviesApp.Models;

namespace MoviesApp.ViewModels
{
    public class AddCinemaVM
    {
        public Cinema Cinema { get; set; }
        public List<int>? SelectedMovies { get; set; }
        public IEnumerable<Movie>Movies { get; set; }
    }

}
