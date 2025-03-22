using Microsoft.EntityFrameworkCore;
using drakek.Model;

namespace drakek.Data
{
    public class DrakekDB : DbContext
    {
        public DbSet<Product> product { get; set; }
        public DbSet<People> people { get; set; }
        public DbSet<Role> role { get; set; }
        public DbSet<Permission> permission { get; set; }
        public DbSet<Storage> storage { get; set; }
        public DbSet<Stock> stock { get; set; }
        public DbSet<Customer> customer { get; set; }
        public DbSet<Coupon> coupon { get; set; }
        public DbSet<Order> order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>()
                .HasKey(s => new { s.product, s.storage });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=KIEIN;Database=drakek;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}