using EC_Produits_Service.Data;
using EC_Produits_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace EC_Produits_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProduitsController : ControllerBase
    {
        private readonly ProduitsDbContext _context;

        public ProduitsController(ProduitsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les produits
        /// </summary>
        /// <returns>Liste des produits</returns>
        /// <response code="200">Retourne la liste des produits</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduits()
        {
            return await _context.Produits.ToListAsync();
        }

        /// <summary>
        /// Récupère un produit par son ID
        /// </summary>
        /// <param name="id">ID du produit</param>
        /// <returns>Le produit demandé</returns>
        /// <response code="200">Retourne le produit demandé</response>
        /// <response code="404">Si le produit n'est pas trouvé</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Produit>> GetProduit(int id)
        {
            var produit = await _context.Produits.FindAsync(id);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        /// <summary>
        /// Crée un nouveau produit
        /// </summary>
        /// <param name="produit">Le produit à créer</param>
        /// <returns>Le produit créé</returns>
        /// <response code="201">Retourne le produit créé</response>
        /// <response code="400">Si le produit est null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            _context.Produits.Add(produit);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduit), new { id = produit.Id }, produit);
        }

        /// <summary>
        /// Met à jour un produit existant
        /// </summary>
        /// <param name="id">ID du produit à mettre à jour</param>
        /// <param name="produit">Les nouvelles données du produit</param>
        /// <returns>Aucun contenu</returns>
        /// <response code="204">Si la mise à jour est réussie</response>
        /// <response code="400">Si l'ID ne correspond pas</response>
        /// <response code="404">Si le produit n'est pas trouvé</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutProduit(int id, Produit produit)
        {
            if (id != produit.Id)
            {
                return BadRequest();
            }

            _context.Entry(produit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Produits.Any(e => e.Id == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Supprime un produit
        /// </summary>
        /// <param name="id">ID du produit à supprimer</param>
        /// <returns>Aucun contenu</returns>
        /// <response code="204">Si la suppression est réussie</response>
        /// <response code="404">Si le produit n'est pas trouvé</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null)
            {
                return NotFound();
            }

            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
