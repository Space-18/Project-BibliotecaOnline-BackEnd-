using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Negocio.DTOs.Add;
using Negocio.Services;

namespace ProjectBackend.Controllers
{
    [ApiController]
    [Route("api/libro/{libroId:int}/comentarios")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentario _comentario;

        public ComentarioController(IComentario comentario)
        {
            _comentario = comentario;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get(int libroId)
        {
            var result = await _comentario.GetAll(libroId);
            return Ok(result);
        }

        [HttpGet("{id:int}",Name ="getOneComentario")]
        public async Task<ActionResult> GetOne(int id)
        {
            var result = await _comentario.GetOne(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int libroId, AddComentarioDTO comentarioDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(x => x.Type == "email").FirstOrDefault();

            if (emailClaim == null)
                return BadRequest("Error. Error con el usuario.");

            var result = await _comentario.Save(libroId, emailClaim, comentarioDTO);

            if (result == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("getOneComentario", new { id = result.Id, libroId = libroId }, result);
        }
    }
}
