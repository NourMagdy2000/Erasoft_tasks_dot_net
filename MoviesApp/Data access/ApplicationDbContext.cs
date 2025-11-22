using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Models;

namespace MoviesApp.Data_access
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options) { }

        public DbSet<MovieActors> MovieActors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie_sub_imgs> Movies_sub_imgs { get; set; }
        public DbSet<Cinema_movies> Cinema_Movies { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  // ← مهم جدًا
        }
    }
    }
