# 🛍️ Boutique Microservices - Plateforme E-Commerce Moderne

**ASP.NET Core 8.0 • Microservices Architecture • API Gateway • JWT Authentication • Stripe Integration**

## 📋 Description

Boutique Microservices est une plateforme e-commerce moderne construite avec une architecture de microservices. Cette solution offre une gestion complète des produits, utilisateurs, commandes, panier et paiements, avec une authentification sécurisée et une intégration Stripe pour les transactions.

## ✨ Fonctionnalités Principales

### 🔐 Authentification & Sécurité
- Authentification JWT avec ASP.NET Core Identity
- Gestion des rôles utilisateurs
- Inscription et connexion sécurisées
- Protection des endpoints sensibles

### 🛒 Gestion E-Commerce
- **Catalogue Produits** : CRUD complet des produits avec catégories
- **Panier Intelligent** : Gestion des articles et quantités
- **Commandes** : Traitement et suivi des commandes
- **Paiements** : Intégration Stripe pour les transactions sécurisées
- **Utilisateurs** : Gestion des profils clients

### 🏗️ Architecture Microservices
- **API Gateway** : Point d'entrée unique avec Ocelot
- **Service Discovery** : Communication inter-services
- **Documentation Swagger** : API centralisée et interactive
- **Logging HTTP** : Traçabilité complète des requêtes

### ⚡ Performance & Scalabilité
- Base de données SQL Server par service
- Communication HTTP asynchrone
- Architecture découplée et extensible
- Support des environnements de développement et production

## 🛠️ Technologies Utilisées

### Backend Core
- **ASP.NET Core 8.0** - Framework web moderne et performant
- **Entity Framework Core** - ORM pour la gestion des données
- **SQL Server** - Base de données relationnelle
- **Ocelot** - API Gateway pour la gestion des routes

### Authentification & Sécurité
- **ASP.NET Core Identity** - Gestion des utilisateurs et rôles
- **JWT (JSON Web Tokens)** - Authentification stateless
- **Stripe** - Plateforme de paiement sécurisée

### Développement & Outils
- **Swagger/OpenAPI** - Documentation interactive des API
- **Newtonsoft.Json** - Sérialisation JSON avancée
- **HttpClient** - Communication inter-services
- **Entity Framework Migrations** - Gestion des schémas de base de données

## 🏗️ Architecture des Microservices

### 📊 Diagramme d'Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    🌐 CLIENT APPLICATIONS                   │
└─────────────────────┬───────────────────────────────────────┘
                      │
┌─────────────────────▼───────────────────────────────────────┐
│                 🚪 API GATEWAY (Ocelot)                     │
│                    Port: 5047                              │
│              • Routing & Load Balancing                    │
│              • Swagger Documentation                       │
│              • Request/Response Logging                    │
└─────────────────────┬───────────────────────────────────────┘
                      │
        ┌─────────────┼─────────────┐
        │             │             │
┌───────▼───┐ ┌───────▼───┐ ┌───────▼───┐
│ 🔐 AUTH   │ │ 👥 USERS  │ │ 📦 PRODUCTS│
│ Port: 5001│ │ Port: 5002│ │ Port: 5003│
│ • JWT     │ │ • Profiles│ │ • Catalog │
│ • Login   │ │ • CRUD    │ │ • Categories│
│ • Register│ │ • Search  │ │ • Images  │
└───────────┘ └───────────┘ └───────────┘
        │             │             │
        └─────────────┼─────────────┘
                      │
        ┌─────────────┼─────────────┐
        │             │             │
┌───────▼───┐ ┌───────▼───┐ ┌───────▼───┐
│ 🛒 CART   │ │ 📋 ORDERS │ │ 💳 PAYMENT│
│ Port: 5004│ │ Port: 5005│ │ Port: 5006│
│ • Add Item│ │ • Create  │ │ • Stripe  │
│ • Remove  │ │ • Status  │ │ • Webhooks│
│ • Update  │ │ • History │ │ • Refunds │
└───────────┘ └───────────┘ └───────────┘
        │             │             │
        └─────────────┼─────────────┘
                      │
┌─────────────────────▼───────────────────────────────────────┐
│                🗄️ SQL SERVER DATABASES                      │
│              • AuthentificationDB                          │
│              • UtilisateursDB                              │
│              • ProduitsDB                                  │
│              • PanierDB                                    │
│              • CommandesDB                                 │
│              • PaiementsDB                                 │
└─────────────────────────────────────────────────────────────┘
```

## 🚀 Installation et Démarrage

### Prérequis
- **.NET 8.0 SDK** ou supérieur
- **SQL Server** (LocalDB, Express, ou Developer Edition)
- **Visual Studio 2022** ou **Visual Studio Code**
- **Git** pour le clonage du repository

### Étapes d'installation

1. **Cloner le repository**
   ```bash
   git clone [URL_DU_REPO]
   cd BoutiqueMicroservices-main
   ```

2. **Configurer les bases de données**
   
   Créez les bases de données suivantes dans SQL Server :
   ```sql
   CREATE DATABASE AuthentificationDB;
   CREATE DATABASE UtilisateursDB;
   CREATE DATABASE ProduitsDB;
   CREATE DATABASE PanierDB;
   CREATE DATABASE CommandesDB;
   CREATE DATABASE PaiementsDB;
   ```

3. **Configurer les chaînes de connexion**
   
   Mettez à jour les fichiers `appsettings.json` dans chaque service :
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=NomDeLaBase;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

4. **Exécuter les migrations**
   ```bash
   # Pour chaque service, exécutez :
   dotnet ef database update
   ```

5. **Démarrer les services**
   
   **Option 1 : Visual Studio**
   - Ouvrez la solution dans Visual Studio
   - Configurez plusieurs projets de démarrage
   - Démarrez tous les services simultanément

   **Option 2 : Terminal**
   ```bash
   # Terminal 1 - API Gateway
   cd EC_Gateway
   dotnet run

   # Terminal 2 - Service Authentification
   cd EC_Authentification_Service
   dotnet run

   # Terminal 3 - Service Utilisateurs
   cd EC_Utilisateurs_Service
   dotnet run

   # Terminal 4 - Service Produits
   cd EC_Produits_Service
   dotnet run

   # Terminal 5 - Service Panier
   cd EC_Panier_Service
   dotnet run

   # Terminal 6 - Service Commandes
   cd EC_Commandes_Service
   dotnet run

   # Terminal 7 - Service Paiement
   cd EC_Paiement_Service
   dotnet run
   ```

6. **Accéder à l'API**
   
   - **API Gateway** : `http://localhost:5047`
   - **Swagger Documentation** : `http://localhost:5047/swagger`

## ⚙️ Configuration

### Variables d'environnement

#### Service d'Authentification
```json
{
  "JwtSettings": {
    "SecretKey": "VotreCleSecreteTresLongueEtSecurisee",
    "Issuer": "BoutiqueMicroservices",
    "Audience": "BoutiqueClients",
    "ExpirationInMinutes": 60
  }
}
```

#### Service de Paiement
```json
{
  "Stripe": {
    "SecretKey": "sk_test_votre_cle_stripe_secrete",
    "PublishableKey": "pk_test_votre_cle_stripe_publique"
  }
}
```

### Configuration Ocelot (API Gateway)

Le fichier `ocelot.json` configure le routage :
```json
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/{everything}",
      "DownstreamScheme": "https",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 5001
        }
      ],
      "UpstreamPathTemplate": "/auth/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ]
    }
  ]
}
```

## 🔌 API Reference

### Base URL
```
http://localhost:5047
```

### 🔐 Authentification (`/auth`)

#### Inscription
```http
POST /auth/api/authentification/register
Content-Type: application/json

{
  "email": "user@example.com",
  "motDePasse": "Password123!",
  "role": "Customer"
}
```

#### Connexion
```http
POST /auth/api/authentification/login
Content-Type: application/json

{
  "email": "user@example.com",
  "motDePasse": "Password123!"
}
```

**Réponse** :
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### 👥 Utilisateurs (`/users`)

#### Récupérer tous les utilisateurs
```http
GET /users/api/utilisateurs
Authorization: Bearer {token}
```

#### Créer un utilisateur
```http
POST /users/api/utilisateurs
Content-Type: application/json
Authorization: Bearer {token}

{
  "nom": "Dupont",
  "prenom": "Jean",
  "email": "jean.dupont@example.com",
  "telephone": "+33123456789"
}
```

### 📦 Produits (`/products`)

#### Récupérer tous les produits
```http
GET /products/api/produits
```

#### Récupérer un produit par ID
```http
GET /products/api/produits/{id}
```

#### Créer un produit
```http
POST /products/api/produits
Content-Type: application/json
Authorization: Bearer {token}

{
  "titre": "iPhone 15 Pro",
  "description": "Dernier smartphone Apple",
  "prix": 1199.99,
  "categorie": "Electronique",
  "imageUrl": "https://example.com/iphone15.jpg",
  "vendeurId": 1
}
```

### 🛒 Panier (`/cart`)

#### Ajouter un article au panier
```http
POST /cart/api/paniers/{userId}/items
Content-Type: application/json
Authorization: Bearer {token}

{
  "produitId": 1,
  "quantite": 2
}
```

#### Récupérer le panier
```http
GET /cart/api/paniers/{userId}
Authorization: Bearer {token}
```

### 📋 Commandes (`/orders`)

#### Créer une commande
```http
POST /orders/api/commandes
Content-Type: application/json
Authorization: Bearer {token}

{
  "utilisateurId": 1,
  "lignesCommande": [
    {
      "produitId": 1,
      "quantite": 2,
      "prixUnitaire": 1199.99
    }
  ]
}
```

### 💳 Paiements (`/payments`)

#### Créer un intent de paiement
```http
POST /payments/api/paiements/create-payment-intent
Content-Type: application/json
Authorization: Bearer {token}

{
  "commandeId": "123",
  "clientId": "user123",
  "montant": 2399.98,
  "devise": "cad"
}
```

## 📁 Structure du Projet

```
BoutiqueMicroservices-main/
├── EC_Gateway/                    # 🚪 API Gateway (Ocelot)
│   ├── Routes/                    # Configuration des routes
│   │   ├── ocelot.global.json    # Configuration globale
│   │   ├── ocelot.authentification.json
│   │   ├── ocelot.users.json
│   │   ├── ocelot.products.json
│   │   ├── ocelot.cart.json
│   │   ├── ocelot.orders.json
│   │   └── ocelot.payments.json
│   └── Program.cs                 # Configuration Ocelot
│
├── EC_Authentification_Service/   # 🔐 Service d'authentification
│   ├── Controllers/
│   │   └── AuthentificationController.cs
│   ├── Models/
│   │   ├── ApplicationUser.cs
│   │   ├── JwtSettings.cs
│   │   ├── LoginModel.cs
│   │   └── RegisterModel.cs
│   ├── Services/
│   │   └── JwtService.cs
│   ├── Data/
│   │   ├── AuthentificationDbContext.cs
│   │   └── DbInitializer.cs
│   └── Migrations/               # Migrations EF Core
│
├── EC_Utilisateurs_Service/       # 👥 Service des utilisateurs
│   ├── Controllers/
│   │   └── UtilisateursController.cs
│   ├── Models/
│   │   └── Utilisateur.cs
│   ├── Data/
│   │   └── UtilisateursDbContext.cs
│   └── Migrations/
│
├── EC_Produits_Service/           # 📦 Service des produits
│   ├── Controllers/
│   │   └── ProduitsController.cs
│   ├── Models/
│   │   └── Produit.cs
│   ├── Services/
│   │   └── ExternalProductService.cs
│   ├── Data/
│   │   ├── ProduitsDbContext.cs
│   │   └── DbInitializer.cs
│   └── Migrations/
│
├── EC_Panier_Service/             # 🛒 Service du panier
│   ├── Controllers/
│   │   └── PaniersController.cs
│   ├── Models/
│   │   ├── Panier.cs
│   │   ├── LignePanier.cs
│   │   └── ArticlePanier.cs
│   ├── Services/
│   │   └── ProduitsService.cs
│   ├── Data/
│   │   └── PanierDbContext.cs
│   └── Migrations/
│
├── EC_Commandes_Service/          # 📋 Service des commandes
│   ├── Controllers/
│   │   └── CommandesController.cs
│   ├── Models/
│   │   ├── Commande.cs
│   │   ├── LigneCommande.cs
│   │   ├── DetailCommande.cs
│   │   └── Facture.cs
│   ├── Services/
│   │   ├── PanierService.cs
│   │   └── PaiementService.cs
│   ├── Data/
│   │   └── CommandesDbContext.cs
│   └── Migrations/
│
├── EC_Paiement_Service/           # 💳 Service des paiements
│   ├── Controllers/
│   │   ├── PaiementController.cs
│   │   └── PaiementsController.cs
│   ├── Models/
│   │   └── Paiement.cs
│   ├── DTOs/
│   │   ├── CreatePaymentIntentRequest.cs
│   │   └── PaymentIntentResponse.cs
│   ├── Data/
│   │   └── PaiementDbContext.cs
│   └── Migrations/
│
└── TemAPI/                        # 🌡️ API de test (Weather)
    ├── Controllers/
    │   └── WeatherForecastController.cs
    └── WeatherForecast.cs
```

## 🛠️ Développement

### Scripts de Développement

```bash
# Restaurer les packages NuGet
dotnet restore

# Construire tous les projets
dotnet build

# Exécuter les tests (si disponibles)
dotnet test

# Nettoyer les builds
dotnet clean
```

### 🔧 Ajout de Nouvelles Fonctionnalités

1. **Nouveau microservice** :
   - Créer un nouveau projet ASP.NET Core
   - Configurer Entity Framework et la base de données
   - Ajouter les routes dans Ocelot
   - Implémenter les contrôleurs et modèles

2. **Nouvelle route dans un service existant** :
   - Ajouter l'endpoint dans le contrôleur
   - Mettre à jour la configuration Ocelot
   - Documenter l'API dans Swagger

3. **Nouvelle base de données** :
   - Créer le DbContext
   - Configurer les modèles
   - Exécuter les migrations

### 💡 Exemple d'Extension

```csharp
// Nouveau contrôleur pour les catégories
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ProduitsDbContext _context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categorie>>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Categorie>> PostCategorie(Categorie categorie)
    {
        _context.Categories.Add(categorie);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCategories), new { id = categorie.Id }, categorie);
    }
}
```

## 🚀 Déploiement

### 🏠 Déploiement Local

```bash
# Construire en mode Release
dotnet build --configuration Release

# Publier chaque service
dotnet publish EC_Gateway --configuration Release --output ./publish/gateway
dotnet publish EC_Authentification_Service --configuration Release --output ./publish/auth
# ... répéter pour chaque service
```

### 🐳 Déploiement Docker

```dockerfile
# Dockerfile pour l'API Gateway
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5047

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EC_Gateway/EC_Gateway.csproj", "EC_Gateway/"]
RUN dotnet restore "EC_Gateway/EC_Gateway.csproj"
COPY . .
WORKDIR "/src/EC_Gateway"
RUN dotnet build "EC_Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EC_Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EC_Gateway.dll"]
```

### 🌐 Déploiement en Production

#### Variables d'environnement
```env
# Base de données
ConnectionStrings__DefaultConnection=Server=prod-server;Database=BoutiqueDB;User Id=user;Password=password;

# JWT
JwtSettings__SecretKey=your-production-secret-key
JwtSettings__Issuer=BoutiqueMicroservices
JwtSettings__Audience=BoutiqueClients
JwtSettings__ExpirationInMinutes=60

# Stripe
Stripe__SecretKey=sk_live_your_live_stripe_key
Stripe__PublishableKey=pk_live_your_live_stripe_key

# Environnement
ASPNETCORE_ENVIRONMENT=Production
```

#### Configuration IIS
- Installer le .NET 8.0 Hosting Bundle
- Configurer les pools d'applications
- Définir les variables d'environnement
- Configurer les certificats SSL

## ⚠️ Limitations Actuelles

- **Communication synchrone** : Les services communiquent via HTTP synchrone
- **Pas de cache** : Aucun système de cache implémenté
- **Pas de monitoring** : Aucun système de monitoring/observabilité
- **Pas de tests** : Aucun test unitaire ou d'intégration
- **Pas de CI/CD** : Pipeline de déploiement manuel

## 🚀 Roadmap & Améliorations

### 🎯 Prochaines Fonctionnalités
- [ ] 🔄 Communication asynchrone avec RabbitMQ/Azure Service Bus
- [ ] 📊 Monitoring avec Application Insights ou Prometheus
- [ ] 🗄️ Cache Redis pour les performances
- [ ] 🧪 Tests unitaires et d'intégration complets
- [ ] 🔐 Authentification OAuth2/OpenID Connect
- [ ] 📱 API GraphQL pour les clients mobiles
- [ ] 🚦 Rate limiting et protection DDoS
- [ ] 📚 Documentation API avec Swagger/OpenAPI avancée
- [ ] ⚡ Optimisation des requêtes et indexation
- [ ] 🔄 Webhooks pour les événements en temps réel
- [ ] 📊 Analytics et reporting avancés
- [ ] 🌍 Support multi-langues et multi-devises

### 🎨 Frontend & UX
- [ ] 🖥️ Interface d'administration web
- [ ] 📱 Application mobile React Native/Flutter
- [ ] 🎨 Dashboard analytics en temps réel
- [ ] 🔍 Moteur de recherche avancé
- [ ] 🛒 Expérience d'achat optimisée

### 🏗️ Architecture & DevOps
- [ ] 🐳 Containerisation complète avec Docker Compose
- [ ] ☸️ Orchestration avec Kubernetes
- [ ] 🔄 Pipeline CI/CD avec Azure DevOps/GitHub Actions
- [ ] 🗄️ Base de données distribuée avec CQRS
- [ ] 🔐 Sécurité avancée avec OWASP
- [ ] 📈 Auto-scaling et load balancing

## 🤝 Contribution

Les contributions sont les bienvenues ! Voici comment contribuer :

1. **Fork** le projet
2. **Créer** une branche pour votre fonctionnalité (`git checkout -b feature/AmazingFeature`)
3. **Commit** vos changements (`git commit -m 'Add some AmazingFeature'`)
4. **Push** vers la branche (`git push origin feature/AmazingFeature`)
5. **Ouvrir** une Pull Request

### 📋 Guidelines de Contribution
- Respecter les conventions de code C# et .NET
- Ajouter des tests pour les nouvelles fonctionnalités
- Documenter les nouvelles API avec XML comments
- Suivre les conventions de commit (Conventional Commits)
- Mettre à jour la documentation README

### 🐛 Signaler un Bug
- Utiliser le template d'issue fourni
- Inclure les étapes de reproduction
- Spécifier l'environnement (OS, .NET version, etc.)
- Ajouter les logs d'erreur si disponibles

## 📄 Licence

**MIT License** - Libre d'utilisation pour tous projets commerciaux et non-commerciaux

## 📞 Support & Contact

- 🐛 **Issues** : [Ouvrir une issue](https://github.com/votre-repo/issues)
- 💬 **Discussions** : [Rejoindre les discussions](https://github.com/votre-repo/discussions)
- 📧 **Email** : support@boutique-microservices.com
- 📖 **Documentation** : [Wiki du projet](https://github.com/votre-repo/wiki)

## 🙏 Remerciements

- **Microsoft** pour ASP.NET Core et Entity Framework
- **Ocelot** pour l'API Gateway
- **Stripe** pour la plateforme de paiement
- **Swagger** pour la documentation API
- **La communauté .NET** pour les outils et bibliothèques

---

## 🎉 Boutique Microservices - Votre plateforme e-commerce nouvelle génération ! 🛍️✨

**Développé avec ❤️ en C# et ASP.NET Core 8.0**

*Architecture microservices • Sécurité JWT • Paiements Stripe • API Gateway Ocelot*
