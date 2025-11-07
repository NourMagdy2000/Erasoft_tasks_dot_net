using Microsoft.EntityFrameworkCore;
using MoviesApp.Models;

namespace MoviesApp.Data_access
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie_sub_imgs> Movies_sub_imgs { get; set; }
        public DbSet<Cinema_movies> Cinema_Movies { get; set; }   

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;initial catalog=movie_app;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
