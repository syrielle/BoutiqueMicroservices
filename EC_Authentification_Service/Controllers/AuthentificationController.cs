using EC_Authentification_Service.Models;
using EC_Authentification_Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EC_Authentification_Service.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion de l'authentification des utilisateurs
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthentificationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtService _jwtService;

        public AuthentificationController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Enregistre un nouvel utilisateur
        /// </summary>
        /// <param name="model">Les informations d'enregistrement de l'utilisateur</param>
        /// <returns>Un message de confirmation ou les erreurs rencontrées</returns>
        /// <response code="200">L'utilisateur a été créé avec succès</response>
        /// <response code="400">Les informations fournies sont invalides</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.MotDePasse);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Ajouter le rôle si fourni
            if (!string.IsNullOrEmpty(model.Role))
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            var token = await _jwtService.GenerateToken(user);

            return Ok(new { token });
        }

        /// <summary>
        /// Authentifie un utilisateur existant
        /// </summary>
        /// <param name="model">Les informations de connexion de l'utilisateur</param>
        /// <returns>Un message de confirmation ou d'erreur</returns>
        /// <response code="200">L'utilisateur est connecté avec succès</response>
        /// <response code="401">Les informations de connexion sont invalides</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new { message = "Email ou mot de passe incorrect." });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.MotDePasse, false);
            if (!result.Succeeded)
                return Unauthorized(new { message = "Email ou mot de passe incorrect." });

            var token = await _jwtService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
