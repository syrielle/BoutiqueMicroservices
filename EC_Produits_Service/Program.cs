using EC_Produits_Service.Data;
using EC_Produits_Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services au conteneur
builder.Services.AddControllers()
    .AddNewtonsoftJson(); // Ajout de NewtonsoftJson pour une meilleure gestion du JSON

// Configuration de la base de données
builder.Services.AddDbContext<ProduitsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuration de HttpClient
builder.Services.AddHttpClient<ExternalProductService>();

// Enregistrer le service ExternalProductService
builder.Services.AddScoped<ExternalProductService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Service Produits API",
        Version = "v1",
        Description = "API pour la gestion des produits"
    });
});

var app = builder.Build();

// Initialiser la base de données
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ProduitsDbContext>();
    EC_Produits_Service.Data.DbInitializer.Initialize(context);
}

// Configurer le pipeline HTTP

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Produits API V1");
        c.RoutePrefix = string.Empty; // Pour accéder à Swagger à la racine
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
