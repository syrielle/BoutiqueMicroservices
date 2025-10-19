using System.ComponentModel.DataAnnotations;

namespace EC_Authentification_Service.Models
{
    /// <summary>
    /// Modèle pour la connexion d'un utilisateur
    /// </summary>
    public class LoginModel
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
        public required string MotDePasse { get; set; }
    }
}
