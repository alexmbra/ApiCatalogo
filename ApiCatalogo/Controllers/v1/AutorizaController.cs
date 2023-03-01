using ApiCatalogo.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiCatalogo.Controllers.v1
{
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class AutorizaController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AutorizaController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = config;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("AutorizaController :: Acessado em : " + DateTime.Now.ToLongDateString());
        }

        /// <summary>
        /// Regista um novo usuário
        /// </summary>
        /// <param name="model">Um objeto UsuarioDTO</param>
        /// <returns>Status 200 e o token para o cliente</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> RegisterUser([FromBody] UsuarioDTO model)
        {
            try
            {
                //if(!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
                //}

                IdentityUser user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                await _signInManager.SignInAsync(user, false);

                return Ok(GerarToken(model));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        /// <summary>
        /// Verifica as credenciais de um usuário
        /// </summary>
        /// <param name="userInfo">Um objeto UsuarioDTO</param>
        /// <returns>Status 200 e o token para o cliente</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Login([FromBody] UsuarioDTO userInfo)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return Ok(GerarToken(userInfo));
                }

                ModelState.AddModelError(string.Empty, "Login inválido...");
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        private UsuarioToken? GerarToken(UsuarioDTO? userInfo)
        {
            if (userInfo is null) return null;

            Claim[]? claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("meuPet", "pipoca"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SymmetricSecurityKey? Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials? credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            string? expiracao = _configuration["TokenConfiguration:ExpireHours"];
            DateTime expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:audience"],
                expires: expiration,
                claims: claims,
                signingCredentials: credentials
                );

            return new UsuarioToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT OK"
            };
        }
    }
}
