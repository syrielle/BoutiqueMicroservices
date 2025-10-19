using Newtonsoft.Json;
using EC_Commandes_Service.Models;

namespace EC_Commandes_Service.Services
{
    public class PaiementService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PaiementService> _logger;

        public PaiementService(HttpClient httpClient, ILogger<PaiementService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpClient.BaseAddress = new Uri("http://localhost:5287/"); // URL du service Paiement
        }

        public async Task<PaiementResultatDto?> EffectuerPaiement(PaiementRequeteDto requete)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/paiements", requete);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PaiementResultatDto>(content);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors du paiement pour la commande");
                return null;
            }
        }
    }

    public class PaiementRequeteDto
    {
        public decimal montant { get; set; }
        public string devise { get; set; } = "EUR";
        public string description { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;
    }

    public class PaiementResultatDto
    {
        public bool succes { get; set; }
        public string transactionId { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
    }
} 