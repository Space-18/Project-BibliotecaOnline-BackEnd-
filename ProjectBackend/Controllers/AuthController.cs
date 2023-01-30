using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Negocio.DTOs;
using Negocio.DTOs.Add;
using Negocio.Services;

namespace ProjectBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(UserCredentials credentials)
        {
            var result = await _auth.Login(credentials);

            if (result == null)
            {
                return BadRequest("Error al iniciar sesión");
            }

            return result;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(UserCredentials credentials)
        {
            var result = await _auth.Register(credentials);
            if (result == null)
            {
                return BadRequest("Error.");
            }

            return result;
        }

        [HttpGet("refreshToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AuthResponse>> Refresh()
        {
            var emailClaim = HttpContext.User.Claims.Where(x => x.Type == "email").FirstOrDefault();

            if (emailClaim == null)
                return BadRequest("Error. Error con el usuario.");

            var result = await _auth.Refresh(emailClaim);

            return result;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,/*eliminar Policy para agregar su cuenta como admin*/ Policy = "admin")]
        [HttpPost("CreateAdmin")]
        public async Task<ActionResult> AdminCreate(AddAdminDTO adminDTO)
        {
            var result = await _auth.AddAdmin(adminDTO.Email);

            if (!result.Errors.IsNullOrEmpty())
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "admin")]
        [HttpPost("RemoveAdmin")]
        public async Task<ActionResult> AdminRemove(AddAdminDTO adminDTO)
        {
            var result = await _auth.RemoveAdmin(adminDTO.Email);

            if (!result.Errors.IsNullOrEmpty())
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
