using EC_Panier_Service.Data;
using EC_Panier_Service.Models;
using EC_Panier_Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EC_Panier_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaniersController : ControllerBase
    {
        private readonly PanierDbContext _context;
        private readonly ProduitsService _produitsService;
        private readonly ILogger<PaniersController> _logger;

        public PaniersController(PanierDbContext context, ProduitsService produitsService, ILogger<PaniersController> logger)
        {
            _context = context;
            _produitsService = produitsService;
            _logger = logger;
        }

        // GET: api/paniers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Panier>>> GetPaniers()
        {
            return await _context.Paniers.ToListAsync();
        }

        // GET: api/paniers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Panier>> GetPanier(int id)
        {
            var panier = await _context.Paniers.FindAsync(id);

            if (panier == null)
            {
                return NotFound();
            }

            return panier;
        }

        // POST: api/paniers/ajouter-produit
        [HttpPost("ajouter-produit")]
        public async Task<ActionResult<Panier>> AjouterProduitAuPanier(int panierId, int produitId, int quantite)
        {
            var panier = await _context.Paniers.FindAsync(panierId);
            if (panier == null)
            {
                return NotFound("Panier non trouvé");
            }

            // Vérifier si le produit existe
            var produitExiste = await _produitsService.VerifierProduitExiste(produitId);
            if (!produitExiste)
            {
                return BadRequest("Produit non trouvé");
            }

            // Obtenir le prix du produit
            var prix = await _produitsService.ObtenirPrixProduit(produitId);
            if (prix == null)
            {
                return BadRequest("Impossible d'obtenir le prix du produit");
            }

            // Ajouter le produit au panier
            var lignePanier = new LignePanier
            {
                PanierId = panierId,
                ProduitId = produitId,
                Quantite = quantite,
                Prix = prix.Value
            };

            _context.LignesPanier.Add(lignePanier);
            await _context.SaveChangesAsync();

            return Ok(lignePanier);
        }

        // POST: api/paniers
        [HttpPost]
        public async Task<ActionResult<Panier>> PostPanier(Panier panier)
        {
            _context.Paniers.Add(panier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPanier), new { id = panier.Id }, panier);
        }

        // PUT: api/paniers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPanier(int id, Panier panier)
        {
            if (id != panier.Id)
            {
                return BadRequest();
            }

            _context.Entry(panier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Paniers.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/paniers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePanier(int id)
        {
            var panier = await _context.Paniers.FindAsync(id);
            if (panier == null)
            {
                return NotFound();
            }

            _context.Paniers.Remove(panier);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
