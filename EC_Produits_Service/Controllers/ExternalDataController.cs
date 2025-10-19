using EC_Produits_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace EC_Produits_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalDataController : ControllerBase
    {
        private readonly ExternalProductService _externalProductService;

        public ExternalDataController(ExternalProductService externalProductService)
        {
            _externalProductService = externalProductService;
        }

        /// <summary>
        /// Importe les produits depuis Fake Store API
        /// </summary>
        [HttpPost("import-products")]
        public async Task<IActionResult> ImportProducts()
        {
            try
            {
                await _externalProductService.ImportProductsFromFakeStore();
                return Ok("Produits importés avec succès");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors de l'importation des produits : {ex.Message}");
            }
        }
    }
} 