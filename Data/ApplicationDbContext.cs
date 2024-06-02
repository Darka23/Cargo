using Cargo.Data.Identity;
using Cargo.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Automobile>? Automobiles { get; set; }
        public DbSet<AutomobileModel>? AutomobileModels { get; set; }
        public DbSet<City>? Cities { get; set; }
        public DbSet<Client>? Clients { get; set; }
        public DbSet<Office>? Offices { get; set; }
        public DbSet<Order>? Orders { get; set; }
    }
}