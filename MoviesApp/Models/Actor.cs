namespace MoviesApp.Models
{
    public class Actor
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Nantionality { get; set; }

        public string Img { get; set; } = "defaultImg.png";


       public ICollection<MovieActors>? MovieActors { get; set; }


    }
}
