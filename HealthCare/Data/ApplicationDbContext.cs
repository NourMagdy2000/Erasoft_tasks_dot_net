using Microsoft.EntityFrameworkCore;

namespace HealthCare.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<HealthCare.Models.Doctor> Doctors { get; set; }
        public DbSet<HealthCare.Models.Patient> Patients { get; set; }
        public DbSet<HealthCare.Models.Apointment> Apointments { get; set; }
        public DbSet<HealthCare.Models.Category> Categories { get; set; }
        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;initial catalog=Health_care;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}