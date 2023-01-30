using Datos.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Negocio.DTOs.Add;
using Negocio.Services;

namespace ProjectBackend.Controllers
{
    [ApiController]
    [Route("api/editorial")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EditorialController : ControllerBase
    {
        private readonly IEditorial _editorial;

        public EditorialController(IEditorial editorial)
        {
            _editorial = editorial;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            var result = await _editorial.GetAll();
            return Ok(result);
        }

        [ResponseCache(Duration = 5)]
        [HttpGet("{id:int}",Name = "getOneEditorial")]
        [AllowAnonymous]
        public async Task<ActionResult> GetOne(int id)
        {
            var result = await _editorial.GetOne(id);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post(AddEditorialDTO editorialDTO)
        {
            var result = await _editorial.Save(editorialDTO);

            if (result == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("getOneEditorial", new { id = result.Id }, result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, AddEditorialDTO editorialDTO)
        {
            var result = await _editorial.Update(id, editorialDTO);

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
            var result = await _editorial.Delete(id);

            if (result == 0)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
