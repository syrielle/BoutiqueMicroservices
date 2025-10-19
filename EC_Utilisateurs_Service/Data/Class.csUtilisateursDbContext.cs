using Microsoft.EntityFrameworkCore;
using EC_Utilisateurs_Service.Models;

namespace EC_Utilisateurs_Service.Data
{
    public class UtilisateursDbContext : DbContext
    {
        public UtilisateursDbContext(DbContextOptions<UtilisateursDbContext> options)
            : base(options)
        {
        }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
