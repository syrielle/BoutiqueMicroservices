using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using MMLib.SwaggerForOcelot;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Configuration de base
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Ajouter le logging HTTP
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

// 1. Déclarer le dossier Routes
var routes = "Routes";
builder.Configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = routes;
});

// 2. Déclarer les services Ocelot + SwaggerForOcelot
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

// Ajouter Swagger de base
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Gateway",
        Version = "v1"
    });
});

// Ajouter le logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Utiliser le logging HTTP
app.UseHttpLogging();

// 3. Swagger UI centralisée
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
    opt.DownstreamSwaggerEndPointBasePath = "/swagger/docs";
    opt.ReConfigureUpstreamSwaggerJson = AlterUpstreamSwaggerJson;
});

// 4. Exécuter Ocelot
app.UseOcelot().Wait();

app.Run();

// Fonction pour modifier le Swagger JSON en amont
static string AlterUpstreamSwaggerJson(HttpContext context, string swaggerJson)
{
    var swagger = System.Text.Json.JsonSerializer.Deserialize<object>(swaggerJson);
    return System.Text.Json.JsonSerializer.Serialize(swagger);
}
