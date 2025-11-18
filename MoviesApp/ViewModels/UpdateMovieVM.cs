using MoviesApp.Models;

namespace MoviesApp.ViewModels
{
    public class UpdateMovieVM
    {

        public Models.Movie Movie { get; set; } = new Models.Movie();
        public List<Models.Category>? Categories { get; set; } = new List<Models.Category>();
        public List<int> MovieActors { get; set; }

        public IEnumerable<Actor> AllActors { get; set; }

    }
}
