using Microsoft.EntityFrameworkCore;
using TravelApp.Data.DomainClasses;
using TravelApp.Models;

namespace TravelApp.Data
{
    public class ApplicationDbContext : DbContext
    {

        
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CustomerService> CustomerServices { get; set; }

        public DbSet<Seyahat> Seyahats { get; set;}

        public DbSet<CustomerServiceTask> CustomerServiceTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           

            base.OnConfiguring(optionsBuilder);
        }

        
        public DbSet<User> Users { get; set; }
        

    }
}
