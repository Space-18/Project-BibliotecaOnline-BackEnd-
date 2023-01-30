using AutoMapper;
using Datos.Entities;
using DATOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Negocio.DTOs;
using Negocio.DTOs.Add;
using Negocio.Services;
using System.Security.Claims;

namespace Negocio.Class
{
    public class ComentarioClass : IComentario
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public ComentarioClass(ApplicationDBContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<ComentarioDTO>> GetAll(int libroId)
        {
            var exist = await _context.Libro.AnyAsync(x => x.Id == libroId);

            if (!exist)
                return null;

            var comentario = await _context.Comentario.Where(x => x.LibroId == libroId).ToListAsync();

            return _mapper.Map<List<ComentarioDTO>>(comentario);
        }

        public async Task<ComentarioDTO> GetOne(int id)
        {
            var comentario = await _context.Comentario.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<ComentarioDTO>(comentario);
        }

        public async Task<ComentarioDTO> Save(int libroId, Claim claim,AddComentarioDTO comentarioDTO)
        {
            var email = claim.Value;

            var usuario = await _userManager.FindByEmailAsync(email:email);

            var exist = await _context.Libro.AnyAsync(x => x.Id == libroId);

            if (!exist)
                return null;

            var comentario = _mapper.Map<Comentario>(comentarioDTO);
            comentario.LibroId = libroId;
            comentario.UsuarioId = usuario.Id;
            comentario.Time = DateTime.Now;
            _context.Add(comentario);
            await _context.SaveChangesAsync();

            var newComentario = _mapper.Map<ComentarioDTO>(comentario);

            return newComentario;
        }

        public Task<int> Delete(int usuarioId, int id)
        {
            throw new NotImplementedException();
        }
    }
}
