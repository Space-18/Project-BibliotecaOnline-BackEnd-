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
    public class LibroClass : ILibro
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly string contenedor1 = "portadas"; 
        private readonly string contenedor2 = "documentos"; 

        public LibroClass(ApplicationDBContext context, IMapper mapper, IFileStorage fileStorage)
        {
            _context = context;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        public async Task<List<LibroDTO>> GetAll()
        {
            var libro = await _context.Libro.ToListAsync();
            return _mapper.Map<List<LibroDTO>>(libro);
        }

        public async Task<LibroDTOWithAutorEditorialComentario> GetOne(int id)
        {
            var libro = await _context.Libro
                .Include(x => x.AutorLibros).ThenInclude(x=>x.Autor)
                .Include(x=>x.EditorialLibros).ThenInclude(x=>x.Editorial)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (libro == null) { return null; }

            if (libro != null)
            {
                libro.AutorLibros = libro.AutorLibros.OrderBy(x => x.Orden).ToList();
                libro.EditorialLibros = libro.EditorialLibros.OrderBy(x => x.Orden).ToList();
            }

            return _mapper.Map<LibroDTOWithAutorEditorialComentario>(libro);
        }

        public async Task<LibroDTO> Save(AddLibroDTO libroDTO)
        {
            if (libroDTO.AutorId == null || libroDTO.EditorialId == null)
                return null;

            var autorId = await _context.Autor.Where(x => libroDTO.AutorId.Contains(x.Id)).Select(x => x.Id).ToListAsync();
            var editorialId = await _context.Editorial.Where(x => libroDTO.EditorialId.Contains(x.Id)).Select(x => x.Id).ToListAsync();

            if (libroDTO.AutorId.Count != autorId.Count || libroDTO.EditorialId.Count != editorialId.Count)
                return null;

            var libro = _mapper.Map<Libro>(libroDTO);

            if (libroDTO.Portada != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await libroDTO.Portada.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();

                    var extension = Path.GetExtension(libroDTO.Portada.FileName);
                    libro.Portada = await _fileStorage.SaveFile(contenido,extension,contenedor1,
                        libroDTO.Portada.ContentType);
                }
            }

            if (libroDTO.Url != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await libroDTO.Url.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();

                    var extension = Path.GetExtension(libroDTO.Url.FileName);
                    libro.Url = await _fileStorage.SaveFile(contenido, extension, contenedor2,
                        libroDTO.Url.ContentType);
                }
            }

            Orden(libro);

            _context.Add(libro);
            await _context.SaveChangesAsync();

            var newLibro = _mapper.Map<LibroDTO>(libro);

            return newLibro;
        }

        public async Task<LibroDTO> Update(int id, AddLibroDTO libroDTO)
        {
            var result = await _context.Libro.Include(x => x.AutorLibros)
                .Include(x => x.EditorialLibros)
                .Include(x=>x.GuardadoLibros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null) { return null; }

            result = _mapper.Map(libroDTO, result);

            if (libroDTO.Portada != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await libroDTO.Portada.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();

                    var extension = Path.GetExtension(libroDTO.Portada.FileName);
                    result.Portada = await _fileStorage.UpdateFile(contenido, extension, contenedor1,
                        result.Portada,
                        libroDTO.Portada.ContentType);
                }
            }

            if (libroDTO.Url != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await libroDTO.Url.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();

                    var extension = Path.GetExtension(libroDTO.Url.FileName);
                    result.Url = await _fileStorage.UpdateFile(contenido, extension, contenedor2,
                        result.Url,
                        libroDTO.Url.ContentType);
                }
            }

            Orden(result);
            await _context.SaveChangesAsync();

            var newLibro = _mapper.Map<LibroDTO>(result);

            return newLibro;
        }

        public async Task<int> Delete(int id)
        {
            var exist = await _context.Libro.Include(x=>x.EditorialLibros)
                .Include(x=>x.AutorLibros)
                .Include(x=>x.GuardadoLibros)
                .Include(x=>x.Comentarios)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (exist == null) 
            {
                return 0;
            } 
            else 
            {
                _context.RemoveRange(exist);
                await _context.SaveChangesAsync();

                await _fileStorage.DeleteFile(exist.Portada, contenedor1);
                await _fileStorage.DeleteFile(exist.Url, contenedor2);

                return id;
            }
        }

        private void Orden(Libro libro)
        {
            if (libro.AutorLibros != null && libro.EditorialLibros != null)
            {
                for (int i = 0; i < libro.AutorLibros.Count; i++)
                {
                    libro.AutorLibros[i].Orden = i;
                }

                for (int i = 0; i < libro.EditorialLibros.Count; i++)
                {
                    libro.EditorialLibros[i].Orden = i;
                }
            }
        }
    }
}
