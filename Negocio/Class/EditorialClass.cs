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
    public class EditorialClass : IEditorial
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public EditorialClass(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EditorialDTO>> GetAll()
        {
            var editorial = await _context.Editorial.ToListAsync();
            return _mapper.Map<List<EditorialDTO>>(editorial);
        }

        public async Task<EditorialDTOWithLibros> GetOne(int id)
        {
            var editorial = await _context.Editorial
                .Include(x => x.EditorialLibros).ThenInclude(x => x.Libro)
                .FirstOrDefaultAsync(x=>x.Id==id);

            if (editorial != null)
            {
                editorial.EditorialLibros = editorial.EditorialLibros.OrderBy(x => x.Orden).ToList();
            }

            return _mapper.Map<EditorialDTOWithLibros>(editorial);
        }

        public async Task<EditorialDTO> Save(AddEditorialDTO editorialDTO)
        {
            var exist = await _context.Editorial.AnyAsync(
                x=>x.Nombre == editorialDTO.Nombre &&
                x.Direccion == editorialDTO.Direccion &&
                x.Telefono == editorialDTO.Telefono);

            if (exist) { return null; }

            var editorial = _mapper.Map<Editorial>(editorialDTO);

            _context.Add(editorial);
            await _context.SaveChangesAsync();

            var newEditorial = _mapper.Map<EditorialDTO>(editorial);

            return newEditorial;
        }

        public async Task<EditorialDTO> Update(int id, AddEditorialDTO editorialDTO)
        {
            var exist = await _context.Editorial.AnyAsync(x => x.Id == id);
            if (!exist) { return null; }

            var editorial = _mapper.Map<Editorial>(editorialDTO);
            editorial.Id = id;

            _context.Update(editorial);
            await _context.SaveChangesAsync();

            return _mapper.Map<EditorialDTO>(editorial);
        }

        public async Task<int> Delete(int id)
        {
            var exist = await _context.Editorial.Include(x=>x.EditorialLibros).FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null) { return 0; }
            else if (exist.EditorialLibros.Count >= 1)
            {
                return 0;
            }

            _context.Remove(new Editorial() { Id = id });
            await _context.SaveChangesAsync();
            return id;
        }
    }
}
