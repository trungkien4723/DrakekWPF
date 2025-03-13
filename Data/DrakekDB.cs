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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=KIEIN;Database=drakek;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}