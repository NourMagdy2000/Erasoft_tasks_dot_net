using Microsoft.EntityFrameworkCore;

namespace To_do_app.Data_access
{
    public class ApplicationDbContext: DbContext
    {
        
        public DbSet<To_do_app.Models.Task> Tasks { get; set; }

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;initial catalog=To_do_app;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
