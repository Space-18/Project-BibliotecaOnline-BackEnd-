using AutoMapper;
using Datos.Entities;
using DATOS;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Negocio.DTOs;
using Negocio.DTOs.Add;
using Negocio.DTOs.With;
using Negocio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Class
{
    public class GuardadoClass : IGuardado
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public GuardadoClass(ApplicationDBContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<GuardadoDTO>> GetAll(Claim claim)
        {
            var email = claim.Value;

            var usuario = await _userManager.FindByEmailAsync(email: email);

            var guardado = await _context.Guardado.Where(x => x.Usuario.Id == usuario.Id && x.Estado == true).ToListAsync();
            return _mapper.Map<List<GuardadoDTO>>(guardado);
        }

        public async Task<GuardadoDTOWithLibros> GetOne(Claim claim, int id)
        {
            var email = claim.Value;

            var usuario = await _userManager.FindByEmailAsync(email: email);

            var guardado = await _context.Guardado.Where(x => x.Usuario.Id == usuario.Id && x.Estado == true)
                 .Include(x => x.GuardadoLibros).ThenInclude(x => x.Libro)
                 .FirstOrDefaultAsync(x => x.Id == id);

            if (guardado != null)
            {
                guardado.GuardadoLibros = guardado.GuardadoLibros.OrderBy(x => x.Orden).ToList();
            }

            return _mapper.Map<GuardadoDTOWithLibros>(guardado);
        }

        public async Task<GuardadoDTO> Save(Claim claim, AddGuardadoDTO guardadoDTO)
        {
            var email = claim.Value;

            var usuario = await _userManager.FindByEmailAsync(email: email);

            var exist = await _context.Guardado.AnyAsync(x=>x.Nombre == guardadoDTO.Nombre);

            if (exist)
                return null;

            var guardado = _mapper.Map<Guardados>(guardadoDTO);

            Ordenar(guardado);

            guardado.UsuarioId = usuario.Id;
            _context.Add(guardado);
            await _context.SaveChangesAsync();

            var newGuardado = _mapper.Map<GuardadoDTO>(guardado);

            return newGuardado;
        }

        public async Task<GuardadoDTO> Update(Claim claim, int id, AddGuardadoDTO guardadoDTO)
        {
            var email = claim.Value;

            var usuario = await _userManager.FindByEmailAsync(email: email);

            var result = await _context.Guardado
                .Where(x=>x.UsuarioId == usuario.Id)
                .Include(x => x.GuardadoLibros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null){ return null; }

            result = _mapper.Map(guardadoDTO, result);

            Ordenar(result);

            await _context.SaveChangesAsync();

            var newGuardado = _mapper.Map<GuardadoDTO>(result);

            return newGuardado;
        }

        public async Task<int> Delete(Claim claim, int id)
        {
            var email = claim.Value;

            var usuario = await _userManager.FindByEmailAsync(email: email);

            var result = await _context.Guardado
                .Where(x => x.UsuarioId == usuario.Id && x.Estado == true)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null) { return 0; }

            result.Estado = false;

            _context.Update(result);
            await _context.SaveChangesAsync();

            return id;
        }

        private void Ordenar(Guardados guardado)
        {
            if (guardado.GuardadoLibros != null)
            {
                for (int i = 0; i < guardado.GuardadoLibros.Count; i++)
                {
                    guardado.GuardadoLibros[i].Orden = i;
                }
            }
        }
    }
}
