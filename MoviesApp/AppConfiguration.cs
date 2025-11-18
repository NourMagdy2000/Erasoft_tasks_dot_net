using Microsoft.EntityFrameworkCore;
using MoviesApp.Data_access;
using MoviesApp.Models;
using MoviesApp.Repos;

namespace MoviesApp
{
    public static class AppConfiguration
    {

        public static void Config(this IServiceCollection services , string connectionString )
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]); 
                //options.UseSqlServer(builder.Configuration["ConnectionStrings : DefaultConnection"]); 
                //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IRepository<Actor>, Repository<Actor>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<Cinema>, Repository<Cinema>>();
            services.AddScoped<IRepository<Movie>, Repository<Movie>>();
            services.AddScoped<IRepository<Cinema_movies>, Repository<Cinema_movies>>();
            services.AddScoped<IRepository<MovieActors>, Repository<MovieActors>>();

            services.AddScoped<IRepository<Movie_sub_imgs>, Repository<Movie_sub_imgs>>();

        }
    }
}
