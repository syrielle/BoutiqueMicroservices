namespace EC_Commandes_Service.Models
{
    public class Commande
    {
        public Commande()
        {
            LignesCommande = new List<LigneCommande>();
        }

        public int Id { get; set; }
        public int UtilisateurId { get; set; }
        public DateTime DateCommande { get; set; }
        public decimal Total { get; set; }
        public string Statut { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;

        public virtual ICollection<LigneCommande> LignesCommande { get; set; }
    }
}
