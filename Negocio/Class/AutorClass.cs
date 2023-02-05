using AutoMapper;
using Datos.Entities;
using DATOS;
using Microsoft.EntityFrameworkCore;
using Negocio.DTOs;
using Negocio.DTOs.Add;
using Negocio.DTOs.With;
using Negocio.Services;

namespace Negocio.Class
{
    public class AutorClass : IAutor
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public AutorClass(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AutorDTO>> GetAll()
        {
            var autor = await _context.Autor.ToListAsync();
            return _mapper.Map<List<AutorDTO>>(autor);
        }

        public async Task<AutorDTOWithLibros> GetOne(int id)
        {
            var autor = await _context.Autor
                .Include(x=>x.AutorLibros).ThenInclude(x=>x.Libro)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (autor != null)
            {
                autor.AutorLibros = autor.AutorLibros.OrderBy(x => x.Orden).ToList();
            }

            return _mapper.Map<AutorDTOWithLibros>(autor);
        }

        public async Task<AutorDTO> Save(AddAutorDTO autorDTO)
        {
            var exist = await _context.Autor.AnyAsync(x=>x.Nombres == autorDTO.Nombres && x.Apellidos == autorDTO.Apellidos);

            if (exist)
            {
                return null;
            }

            var autor = _mapper.Map<Autor>(autorDTO);

            _context.Add(autor);
            await _context.SaveChangesAsync();

            var newAutor = _mapper.Map<AutorDTO>(autor);
            return newAutor;
        }

        public async Task<AutorDTO> Update(int id, AddAutorDTO autorDTO)
        {
            var exits = await _context.Autor.AnyAsync(x=> x.Id == id);

            if (!exits)
                return null;

            var autor = _mapper.Map<Autor>(autorDTO);
            autor.Id = id;

            _context.Update(autor);
            await _context.SaveChangesAsync();

            return _mapper.Map<AutorDTO>(autor);
        }

        public async Task<int> Delete(int id)
        {
            var exist = await _context.Autor.Include(x=>x.AutorLibros).FirstOrDefaultAsync(x => x.Id == id);

            if (exist == null) { return 0; }
            else if (exist.AutorLibros.Count >= 1)
            {
                return 0;
            }

            _context.Remove(new Autor() { Id = id });
            await _context.SaveChangesAsync();
            return id;
        }
    }
}
