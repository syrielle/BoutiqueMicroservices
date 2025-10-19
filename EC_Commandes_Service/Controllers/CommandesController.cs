using EC_Commandes_Service.Data;
using EC_Commandes_Service.Models;
using EC_Commandes_Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EC_Commandes_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandesController : ControllerBase
    {
        private readonly CommandesDbContext _context;
        private readonly PanierService _panierService;
        private readonly PaiementService _paiementService;
        private readonly ILogger<CommandesController> _logger;

        public CommandesController(
            CommandesDbContext context,
            PanierService panierService,
            PaiementService paiementService,
            ILogger<CommandesController> logger)
        {
            _context = context;
            _panierService = panierService;
            _paiementService = paiementService;
            _logger = logger;
        }

        // GET: api/commandes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commande>>> GetCommandes()
        {
            return await _context.Commandes.ToListAsync();
        }

        // GET: api/commandes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Commande>> GetCommande(int id)
        {
            var commande = await _context.Commandes.FindAsync(id);

            if (commande == null)
            {
                return NotFound();
            }

            return commande;
        }

        // POST: api/commandes/creer-depuis-panier
        [HttpPost("creer-depuis-panier")]
        public async Task<ActionResult<Commande>> CreerCommandeDepuisPanier(int panierId, string stripeToken)
        {
            // 1. Récupérer le panier
            var panier = await _panierService.ObtenirPanier(panierId);
            if (panier == null)
            {
                return BadRequest("Panier non trouvé");
            }

            // 2. Calculer le total
            decimal total = panier.lignesPanier.Sum(lp => lp.prix * lp.quantite);

            // 3. Créer la commande
            var commande = new Commande
            {
                UtilisateurId = panier.utilisateurId,
                DateCommande = DateTime.UtcNow,
                Total = total,
                Statut = "En attente de paiement"
            };

            // 4. Ajouter les lignes de commande
            foreach (var lignePanier in panier.lignesPanier)
            {
                var ligneCommande = new LigneCommande
                {
                    ProduitId = lignePanier.produitId,
                    Quantite = lignePanier.quantite,
                    PrixUnitaire = lignePanier.prix
                };
                commande.LignesCommande.Add(ligneCommande);
            }

            // 5. Effectuer le paiement
            var paiementRequete = new PaiementRequeteDto
            {
                montant = total,
                devise = "EUR",
                description = $"Commande #{commande.Id}",
                token = stripeToken
            };

            var resultatPaiement = await _paiementService.EffectuerPaiement(paiementRequete);
            if (resultatPaiement == null || !resultatPaiement.succes)
            {
                return BadRequest("Échec du paiement");
            }

            // 6. Mettre à jour le statut de la commande
            commande.Statut = "Payée";
            commande.TransactionId = resultatPaiement.transactionId;

            // 7. Sauvegarder la commande
            _context.Commandes.Add(commande);
            await _context.SaveChangesAsync();

            // 8. Vider le panier
            await _panierService.ViderPanier(panierId);

            return CreatedAtAction(nameof(GetCommande), new { id = commande.Id }, commande);
        }

        // PUT: api/commandes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommande(int id, Commande commande)
        {
            if (id != commande.Id)
            {
                return BadRequest();
            }

            _context.Entry(commande).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandeExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        private bool CommandeExists(int id)
        {
            return _context.Commandes.Any(e => e.Id == id);
        }

        // DELETE: api/commandes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommande(int id)
        {
            var commande = await _context.Commandes.FindAsync(id);
            if (commande == null)
            {
                return NotFound();
            }

            _context.Commandes.Remove(commande);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
