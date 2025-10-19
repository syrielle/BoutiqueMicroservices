using Microsoft.EntityFrameworkCore;
using EC_Commandes_Service.Models;

namespace EC_Commandes_Service.Data
{
    public class CommandesDbContext : DbContext
    {
        public CommandesDbContext(DbContextOptions<CommandesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Commande> Commandes { get; set; }
        public DbSet<DetailCommande> DetailsCommande { get; set; }
        public DbSet<Facture> Factures { get; set; }
    }
}
