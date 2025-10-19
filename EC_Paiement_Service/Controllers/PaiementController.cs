using EC_Paiement_Service.Data;
using EC_Paiement_Service.DTOs;
using EC_Paiement_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using Stripe.Events;

namespace EC_Paiement_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaiementController : ControllerBase
    {
        private readonly PaiementDbContext _context;
        private readonly IConfiguration _configuration;

        public PaiementController(PaiementDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Crée une intention de paiement Stripe
        /// </summary>
        /// <param name="request">Les informations du paiement</param>
        /// <returns>Les informations nécessaires pour le paiement côté client</returns>
        [HttpPost("create-payment-intent")]
        public async Task<ActionResult<PaymentIntentResponse>> CreatePaymentIntent([FromBody] CreatePaymentIntentRequest request)
        {
            try
            {
                // Créer l'intention de paiement avec Stripe
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(request.Montant * 100), // Stripe utilise les centimes
                    Currency = request.Devise,
                    PaymentMethodTypes = new List<string> { "card" },
                    Metadata = new Dictionary<string, string>
                    {
                        { "CommandeId", request.CommandeId },
                        { "ClientId", request.ClientId }
                    }
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                // Enregistrer le paiement dans la base de données
                var paiement = new Paiement
                {
                    CommandeId = request.CommandeId,
                    ClientId = request.ClientId,
                    Montant = request.Montant,
                    Devise = request.Devise,
                    Statut = "en_attente",
                    StripePaymentIntentId = paymentIntent.Id
                };

                _context.Paiements.Add(paiement);
                await _context.SaveChangesAsync();

                // Retourner la réponse
                return Ok(new PaymentIntentResponse
                {
                    ClientSecret = paymentIntent.ClientSecret,
                    PaymentIntentId = paymentIntent.Id,
                    PublishableKey = _configuration["Stripe:PublishableKey"]
                });
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        /// <summary>
        /// Webhook pour recevoir les événements Stripe
        /// </summary>
        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _configuration["Stripe:WebhookSecret"]
            );

            if (stripeEvent.Type == "payment_intent.succeeded")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                var paiement = await _context.Paiements
                    .FirstOrDefaultAsync(p => p.StripePaymentIntentId == paymentIntent.Id);

                if (paiement != null)
                {
                    paiement.Statut = "paye";
                    paiement.DateMiseAJour = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }
            }

            return Ok();
        }

        /// <summary>
        /// Récupère le statut d'un paiement
        /// </summary>
        /// <param name="paymentIntentId">L'ID de l'intention de paiement Stripe</param>
        [HttpGet("status/{paymentIntentId}")]
        public async Task<ActionResult<string>> GetPaymentStatus(string paymentIntentId)
        {
            var paiement = await _context.Paiements
                .FirstOrDefaultAsync(p => p.StripePaymentIntentId == paymentIntentId);

            if (paiement == null)
            {
                return NotFound();
            }

            return Ok(paiement.Statut);
        }
    }
} 