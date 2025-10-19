using EC_Produits_Service.Models;

namespace EC_Produits_Service.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ProduitsDbContext context)
        {
            context.Database.EnsureCreated();

            // Vérifier si la base de données contient déjà des produits
            if (context.Produits.Any())
            {
                return; // La base de données a déjà été initialisée
            }

            var produits = new Produit[]
            {
                new Produit
                {
                    Titre = "Ordinateur Portable",
                    Description = "Ordinateur portable haute performance",
                    Prix = 999.99m,
                    Categorie = "Informatique",
                    ImageUrl = "https://example.com/laptop.jpg",
                    VendeurId = 1
                },
                new Produit
                {
                    Titre = "Smartphone",
                    Description = "Smartphone dernier cri",
                    Prix = 699.99m,
                    Categorie = "Téléphonie",
                    ImageUrl = "https://example.com/phone.jpg",
                    VendeurId = 1
                },
                new Produit
                {
                    Titre = "Casque Audio",
                    Description = "Casque sans fil avec réduction de bruit",
                    Prix = 199.99m,
                    Categorie = "Audio",
                    ImageUrl = "https://example.com/headphones.jpg",
                    VendeurId = 2
                }
            };

            context.Produits.AddRange(produits);
            context.SaveChanges();
        }
    }
} 