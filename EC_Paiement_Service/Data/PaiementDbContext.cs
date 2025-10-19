using Microsoft.EntityFrameworkCore;
using EC_Paiement_Service.Models;

namespace EC_Paiement_Service.Data
{
    public class PaiementDbContext : DbContext
    {
        public PaiementDbContext(DbContextOptions<PaiementDbContext> options)
            : base(options)
        {
        }

        public DbSet<Paiement> Paiements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paiement>()
                .Property(p => p.Montant)
                .HasPrecision(18, 2);
        }
    }
}
