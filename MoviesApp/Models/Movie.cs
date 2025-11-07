namespace MoviesApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool status { get; set; }
        public decimal price { get; set; } 

        public string img {  get; set; }

        public Category Category { get; set; } = new Category();


    }
}
