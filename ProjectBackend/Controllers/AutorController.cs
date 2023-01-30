using Datos.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Negocio.DTOs.Add;
using Negocio.Services;

namespace ProjectBackend.Controllers
{
    [ApiController]
    [Route("api/autor")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AutorController : ControllerBase
    {
        private readonly IAutor _autor;

        public AutorController(IAutor autor)
        {
            _autor = autor;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            var result = await _autor.GetAll();
            return Ok(result);
        }

        [ResponseCache(Duration =5)]
        [HttpGet("{id:int}",Name ="getOneAutor")]
        [AllowAnonymous]
        public async Task<ActionResult> GetOne(int id)
        {
            var result = await _autor.GetOne(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "admin")]
        public async Task<ActionResult> Post([FromBody] AddAutorDTO autorDTO)
        {
            var result = await _autor.Save(autorDTO);

            if (result == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("getOneAutor", new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "admin")]
        public async Task<ActionResult> Put(int id, AddAutorDTO autorDTO)
        {
            var result = await _autor.Update(id,autorDTO);

            if (result == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _autor.Delete(id);

            if (result == 0)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
