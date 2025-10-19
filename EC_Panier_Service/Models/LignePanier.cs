namespace EC_Panier_Service.Models
{
    public class LignePanier
    {
        public int Id { get; set; }
        public int PanierId { get; set; }
        public int ProduitId { get; set; }
        public int Quantite { get; set; }
        public decimal Prix { get; set; }
        
        public virtual Panier? Panier { get; set; }
    }
} 