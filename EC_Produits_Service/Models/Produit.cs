namespace EC_Produits_Service.Models
{
    public class Produit
    {
        public int Id { get; set; } 
        public string Titre { get; set; }    = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Prix { get; set; }
        public string Categorie { get; set; } = string.Empty;
        public string ImageUrl { get; set; }=string.Empty;
        public int VendeurId { get; set; }
    }
}
