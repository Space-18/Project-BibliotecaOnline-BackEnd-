using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Negocio.DTOs.Add;
using Negocio.Services;

namespace ProjectBackend.Controllers
{
    [ApiController]
    [Route("api/libro")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LibroController : ControllerBase
    {
        private readonly ILibro _libro;

        public LibroController(ILibro libro)
        {
            _libro = libro;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            var result = await _libro.GetAll();
            return Ok(result);
        }

        [ResponseCache(Duration = 5)]
        [HttpGet("{id:int}",Name = "getOneLibro")]
        [AllowAnonymous]
        public async Task<ActionResult> GetOne(int id)
        {
            var result = await _libro.GetOne(id);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] AddLibroDTO libroDTO)
        {
            var result = await _libro.Save(libroDTO);

            if (result == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("getOneLibro", new { id = result.Id }, result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] AddLibroDTO libroDTO)
        {
            var result = await _libro.Update(id, libroDTO);

            if (result == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _libro.Delete(id);

            if (result == 0)
            {return BadRequest();}

            return NoContent();
        }
    }
}
