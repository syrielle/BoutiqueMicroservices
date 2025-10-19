using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EC_Authentification_Service.Models;

namespace EC_Authentification_Service.Data
{
    public class AuthentificationDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthentificationDbContext(DbContextOptions<AuthentificationDbContext> options)
            : base(options)
        {
        }
    }
}
  