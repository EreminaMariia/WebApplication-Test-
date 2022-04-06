using Microsoft.EntityFrameworkCore;
using WebApplicationTest.Entities;

namespace WebApplicationTest
{
    public class ApplicationContext : DbContext
    {
        public DbSet<District> Districts { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=AppDB;Trusted_Connection=True;");
        }
    }
}
