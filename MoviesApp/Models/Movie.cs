namespace MoviesApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public decimal Price { get; set; } 

        public string ? Img {  get; set; }
        public int CategoryId { get; set; }
        public Category ?Category { get; set; }
        public ICollection<Movie_sub_imgs> movie_Sub_Imgs { get; set; }


    }
}
