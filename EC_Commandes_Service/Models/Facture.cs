namespace EC_Commandes_Service.Models
{
    public class Facture
    {
        public int Id { get; set; }
        public int CommandeId { get; set; }
        public DateTime DateFacture { get; set; }
        public decimal Montant { get; set; }
    }
}
