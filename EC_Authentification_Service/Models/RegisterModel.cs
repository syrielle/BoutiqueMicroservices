using System.ComponentModel.DataAnnotations;

namespace EC_Authentification_Service.Models
{
    /// <summary>
    /// Modèle pour l'enregistrement d'un nouvel utilisateur
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// L'adresse email de l'utilisateur
        /// </summary>
        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public required string Email { get; set; }

        /// <summary>
        /// Le mot de passe de l'utilisateur
        /// </summary>
        [Required(ErrorMessage = "Le mot de passe est requis")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères")]
        public required string MotDePasse { get; set; }

        /// <summary>
        /// Le rôle de l'utilisateur (optionnel)
        /// </summary>
        public string? Role { get; set; }
    }
}
