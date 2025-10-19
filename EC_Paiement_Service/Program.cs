using EC_Paiement_Service.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Configuration de Stripe
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

// Ajouter les services au conteneur
builder.Services.AddControllers()
    .AddNewtonsoftJson(); // Ajout de NewtonsoftJson pour une meilleure gestion du JSON

// Configuration de la base de données
builder.Services.AddDbContext<PaiementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Service Paiement API",
        Version = "v1",
        Description = "API pour la gestion des paiements avec Stripe"
    });
});

var app = builder.Build();

// Configurer le pipeline HTTP

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Paiement API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
