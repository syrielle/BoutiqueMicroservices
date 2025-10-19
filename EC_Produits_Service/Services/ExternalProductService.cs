using System.Net.Http.Json;
using EC_Produits_Service.Data;
using EC_Produits_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace EC_Produits_Service.Services
{
    public class ExternalProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ProduitsDbContext _context;
        private readonly ILogger<ExternalProductService> _logger;

        public ExternalProductService(HttpClient httpClient, ProduitsDbContext context, ILogger<ExternalProductService> logger)
        {
            _httpClient = httpClient;
            _context = context;
            _logger = logger;
        }

        public async Task ImportProductsFromFakeStore()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<FakeStoreProduct>>("https://fakestoreapi.com/products");
                
                if (products == null)
                {
                    _logger.LogError("Aucun produit n'a été récupéré de Fake Store API");
                    return;
                }

                foreach (var fakeProduct in products)
                {
                    // Vérifier si le produit existe déjà
                    var existingProduct = await _context.Produits
                        .FirstOrDefaultAsync(p => p.Titre == fakeProduct.title);

                    if (existingProduct == null)
                    {
                        var product = new Produit
                        {
                            Titre = fakeProduct.title,
                            Description = fakeProduct.description,
                            Prix = fakeProduct.price,
                            Categorie = fakeProduct.category,
                            ImageUrl = fakeProduct.image
                        };

                        _context.Produits.Add(product);
                    }
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation($"{products.Count} produits ont été importés avec succès");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'importation des produits depuis Fake Store API");
                throw;
            }
        }
    }

    // Classe pour désérialiser les données de Fake Store API
    public class FakeStoreProduct
    {
        public int id { get; set; }
        public required string title { get; set; }
        public decimal price { get; set; }
        public required string description { get; set; }
        public required string category { get; set; }
        public required string image { get; set; }
    }
} 