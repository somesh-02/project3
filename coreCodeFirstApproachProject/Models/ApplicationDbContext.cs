using Microsoft.EntityFrameworkCore;

namespace coreCodeFirstApproachProject.Models
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }  
        public DbSet<User1> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=HSC-PF25NJE3;Database=ProjectDB;Integrated Security=true;TrustServerCertificate=true");
        }
    }
}
