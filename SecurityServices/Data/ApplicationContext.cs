using Microsoft.EntityFrameworkCore;
using SecurityServices.Models;

namespace SecurityServices.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options) { }

        public DbSet<Employee> Employees  { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vacancy> Vacancy { get; set; }
        public DbSet<SecurityServices.Models.Testimonial> Testimonial { get; set; } = default!;
    }
}
