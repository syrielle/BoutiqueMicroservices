namespace EC_Panier_Service.Models
{
    public class Panier
    {
        public Panier()
        {
            LignesPanier = new List<LignePanier>();
        }

        public int Id { get; set; }
        public int UtilisateurId { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.UtcNow;
        public virtual ICollection<LignePanier> LignesPanier { get; set; }
    }
}
