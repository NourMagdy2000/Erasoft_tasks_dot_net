namespace MoviesApp.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }

       public ICollection<Cinema_movies> ?Cinema_Movies { get; set; }
    }
}
