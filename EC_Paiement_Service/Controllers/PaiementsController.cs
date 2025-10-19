using EC_Paiement_Service.Data;
using EC_Paiement_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EC_Paiement_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaiementsController : ControllerBase
    {
        private readonly PaiementDbContext _context;

        public PaiementsController(PaiementDbContext context)
        {
            _context = context;
        }

        // GET: api/paiements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paiement>>> GetPaiements()
        {
            return await _context.Paiements.ToListAsync();
        }

        // GET: api/paiements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paiement>> GetPaiement(int id)
        {
            var paiement = await _context.Paiements.FindAsync(id);

            if (paiement == null)
            {
                return NotFound();
            }

            return paiement;
        }

        // POST: api/paiements
        [HttpPost]
        public async Task<ActionResult<Paiement>> PostPaiement(Paiement paiement)
        {
            _context.Paiements.Add(paiement);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaiement), new { id = paiement.Id }, paiement);
        }

        // PUT: api/paiements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaiement(int id, Paiement paiement)
        {
            if (id != paiement.Id)
            {
                return BadRequest();
            }

            _context.Entry(paiement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Paiements.Any(e => e.Id == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/paiements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaiement(int id)
        {
            var paiement = await _context.Paiements.FindAsync(id);
            if (paiement == null)
            {
                return NotFound();
            }

            _context.Paiements.Remove(paiement);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
