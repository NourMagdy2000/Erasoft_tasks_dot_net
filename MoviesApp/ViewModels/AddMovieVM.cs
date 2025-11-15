namespace MoviesApp.ViewModels
{
    public class AddMovieVM
    {
        public Models.Movie Movie { get; set; } = new Models.Movie();
        public List<Models.Category>? Categories { get; set; } = new List<Models.Category>();
    }
}
