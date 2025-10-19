using System.ComponentModel.DataAnnotations;

namespace EC_Paiement_Service.DTOs
{
    public class CreatePaymentIntentRequest
    {
        [Required]
        public string CommandeId { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le montant doit être supérieur à 0")]
        public decimal Montant { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Devise { get; set; } = "cad";
    }
} 