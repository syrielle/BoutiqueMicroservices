namespace EC_Utilisateurs_Service.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string Telephone { get; set; }=string.Empty;
        public string Role { get; set; } = string.Empty; // "client" ou "vendeur"
    }
}
