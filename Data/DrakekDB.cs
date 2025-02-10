using Microsoft.EntityFrameworkCore;
using drakek.Model;

namespace drakek.Data
{
    public class DrakekDB : DbContext
    {
        public DbSet<Product> product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=KIEIN;Database=drakek;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}