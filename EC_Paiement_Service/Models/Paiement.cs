using System.ComponentModel.DataAnnotations;

namespace EC_Paiement_Service.Models
{
    public class Paiement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CommandeId { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public decimal Montant { get; set; }

        [Required]
        public string Devise { get; set; } = "cad";

        [Required]
        public string Statut { get; set; }

        public string? StripePaymentIntentId { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

        public DateTime? DateMiseAJour { get; set; }
    }
}
