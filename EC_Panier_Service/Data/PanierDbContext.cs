using Microsoft.EntityFrameworkCore;
using EC_Panier_Service.Models;

namespace EC_Panier_Service.Data
{
    public class PanierDbContext : DbContext
    {
        public PanierDbContext(DbContextOptions<PanierDbContext> options)
            : base(options)
        {
        }

        public DbSet<Panier> Paniers { get; set; }
        public DbSet<ArticlePanier> ArticlesPanier { get; set; }
        public DbSet<LignePanier> LignesPanier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LignePanier>()
                .HasOne(lp => lp.Panier)
                .WithMany(p => p.LignesPanier)
                .HasForeignKey(lp => lp.PanierId);

            modelBuilder.Entity<LignePanier>()
                .Property(lp => lp.Prix)
                .HasPrecision(18, 2);
        }
    }
}
