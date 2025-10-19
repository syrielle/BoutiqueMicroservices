namespace EC_Panier_Service.Models
{
    public class ArticlePanier
    {
        public int Id { get; set; }
        public int PanierId { get; set; }
        public int ProduitId { get; set; }
        public int Quantite { get; set; }
    }
}
