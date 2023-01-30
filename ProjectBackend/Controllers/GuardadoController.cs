using Datos.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Negocio.DTOs.Add;
using Negocio.DTOs.With;
using Negocio.Services;

namespace ProjectBackend.Controllers
{
    [ApiController]
    [Route("api/usuario/guardado")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GuardadoController : ControllerBase
    {
        private readonly IGuardado _guardado;

        public GuardadoController(IGuardado guardado)
        {
            _guardado = guardado;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var emailClaim = HttpContext.User.Claims.Where(x => x.Type == "email").FirstOrDefault();

            if (emailClaim == null)
                return BadRequest("Error. Error con el usuario.");

            var result = await _guardado.GetAll(emailClaim);
            return Ok(result);
        }

        [HttpGet("{id:int}", Name = "getOneGuardado")]
        public async Task<ActionResult> GenOne(int id)
        {
            var emailClaim = HttpContext.User.Claims.Where(x => x.Type == "email").FirstOrDefault();

            if (emailClaim == null)
                return BadRequest("Error. Error con el usuario.");

            var result = await _guardado.GetOne(emailClaim, id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(AddGuardadoDTO guardadoDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(x => x.Type == "email").FirstOrDefault();

            if (emailClaim == null)
                return BadRequest("Error. Error con el usuario.");

            var result = await _guardado.Save(emailClaim, guardadoDTO);

            if (result == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("getOneGuardado", new { id = result.Id  }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, AddGuardadoDTO guardadoDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(x => x.Type == "email").FirstOrDefault();

            if (emailClaim == null)
                return BadRequest("Error. Error con el usuario.");

            var result = await _guardado.Update(emailClaim, id, guardadoDTO);

            if (result == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var emailClaim = HttpContext.User.Claims.Where(x => x.Type == "email").FirstOrDefault();

            if (emailClaim == null)
                return BadRequest("Error. Error con el usuario.");

            var result = await _guardado.Delete(emailClaim, id);

            if (result == 0)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
