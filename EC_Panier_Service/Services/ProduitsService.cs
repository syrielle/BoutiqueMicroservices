using Newtonsoft.Json;
using EC_Panier_Service.Models;

namespace EC_Panier_Service.Services
{
    public class ProduitsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProduitsService> _logger;

        public ProduitsService(HttpClient httpClient, ILogger<ProduitsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpClient.BaseAddress = new Uri("http://localhost:5013/"); // URL du service Produits
        }

        public async Task<bool> VerifierProduitExiste(int produitId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/produits/{produitId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la vérification du produit {ProduitId}", produitId);
                return false;
            }
        }

        public async Task<decimal?> ObtenirPrixProduit(int produitId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/produits/{produitId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var produit = JsonConvert.DeserializeObject<ProduitDto>(content);
                    return produit?.prix;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération du prix du produit {ProduitId}", produitId);
                return null;
            }
        }
    }

    // Classe DTO pour désérialiser les produits
    public class ProduitDto
    {
        public int id { get; set; }
        public string titre { get; set; } = string.Empty;
        public decimal prix { get; set; }
        public string description { get; set; } = string.Empty;
        public string categorie { get; set; } = string.Empty;
        public string imageUrl { get; set; } = string.Empty;
        public int vendeurId { get; set; }
    }
} 