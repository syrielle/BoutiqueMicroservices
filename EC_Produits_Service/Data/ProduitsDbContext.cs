using Microsoft.EntityFrameworkCore;
using EC_Produits_Service.Models;

namespace EC_Produits_Service.Data
{
    public class ProduitsDbContext : DbContext
    {
        public ProduitsDbContext(DbContextOptions<ProduitsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produit> Produits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produit>()
                .Property(p => p.Prix)
                .HasPrecision(18, 2);
        }
    }
}
