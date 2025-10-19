using Newtonsoft.Json;
using EC_Commandes_Service.Models;

namespace EC_Commandes_Service.Services
{
    public class PanierService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PanierService> _logger;

        public PanierService(HttpClient httpClient, ILogger<PanierService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpClient.BaseAddress = new Uri("http://localhost:5030/"); // URL du service Panier
        }

        public async Task<PanierDto?> ObtenirPanier(int panierId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/paniers/{panierId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PanierDto>(content);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération du panier {PanierId}", panierId);
                return null;
            }
        }

        public async Task<bool> ViderPanier(int panierId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/paniers/{panierId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression du panier {PanierId}", panierId);
                return false;
            }
        }
    }

    // DTOs pour la désérialisation
    public class PanierDto
    {
        public int id { get; set; }
        public int utilisateurId { get; set; }
        public DateTime dateCreation { get; set; }
        public List<LignePanierDto> lignesPanier { get; set; } = new();
    }

    public class LignePanierDto
    {
        public int id { get; set; }
        public int panierId { get; set; }
        public int produitId { get; set; }
        public int quantite { get; set; }
        public decimal prix { get; set; }
    }
} 