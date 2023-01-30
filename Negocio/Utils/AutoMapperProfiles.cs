using AutoMapper;
using Datos.Entities;
using Negocio.DTOs;
using Negocio.DTOs.Add;
using Negocio.DTOs.With;

namespace Negocio.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Autor
            CreateMap<AddAutorDTO, Autor>();
            CreateMap<Autor, AutorDTO>();
            CreateMap<Autor, AutorDTOWithLibros>().ForMember(x => x.Libros, y => y.MapFrom(MapAutorDTOLibros));
            #endregion

            #region Libro
            CreateMap<AddLibroDTO, Libro>().ForMember(x => x.AutorLibros, y => y.MapFrom(MapAutorLibro))
                .ForMember(x => x.EditorialLibros, y => y.MapFrom(MapEditorialLibro))
                .ForMember(x => x.Portada, y => y.Ignore())
                .ForMember(x => x.Url, y => y.Ignore());
            CreateMap<Libro, LibroDTO>();
            CreateMap<Libro, LibroDTOWithGuardado>().ForMember(x => x.Guardados, y => y.MapFrom(MapLibroDTOGuardado));
            CreateMap<Libro, LibroDTOWithAutorEditorialComentario>().ForMember(x => x.Autores, y => y.MapFrom(MapLibroDTOAutores))
                .ForMember(x => x.Editoriales, y => y.MapFrom(MapLibroDTOEditoriales));
            #endregion

            #region Comentario
            CreateMap<AddComentarioDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
            #endregion

            #region Editorial
            CreateMap<AddEditorialDTO, Editorial>();
            CreateMap<Editorial, EditorialDTO>();
            CreateMap<Editorial, EditorialDTOWithLibros>()
                .ForMember(x => x.Libros, y => y.MapFrom(MapEditorialDTOlibros));
            #endregion

            #region Guardado
            CreateMap<AddGuardadoDTO, Guardados>().ForMember(x => x.GuardadoLibros, y => y.MapFrom(MapGuardadoLibro));
            CreateMap<Guardados, GuardadoDTO>();
            CreateMap<Guardados, GuardadoDTOWithLibros>().ForMember(x => x.Libros, y => y.MapFrom(MapGuardadoDTOLibros));
            #endregion
        }

        #region Methods
        private List<AutorLibro> MapAutorLibro(AddLibroDTO libroDTO, Libro libro)
        {
            var result = new List<AutorLibro>();

            if (libroDTO.AutorId == null)
                return result;

            foreach (var item in libroDTO.AutorId)
            {
                result.Add(new AutorLibro() { AutorId = item });
            }

            return result;
        }

        private List<EditorialLibro> MapEditorialLibro(AddLibroDTO libroDTO, Libro libro)
        {
            var result = new List<EditorialLibro>();

            if (libroDTO.EditorialId == null)
                return result;

            foreach (var item in libroDTO.EditorialId)
            {
                result.Add(new EditorialLibro() { EditorialId = item });
            }

            return result;
        }

        private List<AutorDTO> MapLibroDTOAutores(Libro libro, LibroDTO libroDTO)
        {
            var result = new List<AutorDTO>();

            if (libro.AutorLibros == null)
            {
                return result;
            }

            foreach (var item in libro.AutorLibros)
            {
                result.Add(new AutorDTO()
                {
                    Id = item.AutorId,
                    Nombres = item.Autor.Nombres,
                    Apellidos = item.Autor.Apellidos,
                    Nacionalidad = item.Autor.Nacionalidad
                });
            }

            MapLibroDTOEditoriales(libro, libroDTO);

            return result;
        }
        
        private List<GuardadoDTO> MapLibroDTOGuardado(Libro libro, LibroDTO libroDTO)
        {
            var result = new List<GuardadoDTO>();

            if (libro.GuardadoLibros == null)
            {
                return result;
            }

            foreach (var item in libro.GuardadoLibros)
            {
                result.Add(new GuardadoDTO()
                {
                    Id = item.GuardadoId,
                    Nombre = item.Guardado.Nombre
                });
            }

            MapLibroDTOEditoriales(libro, libroDTO);

            return result;
        }
        
        private List<EditorialDTO> MapLibroDTOEditoriales(Libro libro, LibroDTO libroDTO)
        {
            var result = new List<EditorialDTO>();

            if (libro.EditorialLibros == null)
            {
                return result;
            }

            foreach (var item in libro.EditorialLibros)
            {
                result.Add(new EditorialDTO()
                {
                    Id = item.EditorialId,
                    Nombre = item.Editorial.Nombre
                });
            }

            return result;
        }

        private List<LibroDTO> MapAutorDTOLibros(Autor autor, AutorDTO autorDTO)
        {
            var result = new List<LibroDTO>();

            if (autor.AutorLibros == null)
            {
                return result;
            }

            foreach (var item in autor.AutorLibros)
            {
                result.Add(new LibroDTO()
                {
                    Id = item.LibroId,
                    Nombre = item.Libro.Nombre,
                    Portada = item.Libro.Portada,
                    Url = item.Libro.Url
                });
            }

            return result;
        }

        private List<LibroDTO> MapEditorialDTOlibros(Editorial editorial, EditorialDTO editorialDTO)
        {
            var result = new List<LibroDTO>();

            if (editorial.EditorialLibros == null)
            {
                return result;
            }

            foreach (var item in editorial.EditorialLibros)
            {
                result.Add(new LibroDTO()
                {
                    Id = item.LibroId,
                    Nombre = item.Libro.Nombre,
                    Portada = item.Libro.Portada,
                    Url = item.Libro.Url
                });
            }

            return result;
        }

        private List<GuardadoLibro> MapGuardadoLibro(AddGuardadoDTO guardadoDTO, Guardados guardados)
        {
            var result = new List<GuardadoLibro>();

            if (guardadoDTO.LibrosId == null)
                return result;

            foreach (var item in guardadoDTO.LibrosId)
            {
                result.Add(new GuardadoLibro() { LibroId = item });
            }
            return result;
        }

        private List<LibroDTO> MapGuardadoDTOLibros(Guardados guardados, GuardadoDTO guardadoDTO)
        {
            var result = new List<LibroDTO>();

            if (guardados.GuardadoLibros == null)
            {
                return result;
            }

            foreach (var item in guardados.GuardadoLibros)
            {
                result.Add(new LibroDTO()
                {
                    Id = item.LibroId,
                    Nombre = item.Libro.Nombre,
                    Portada = item.Libro.Portada,
                    Url = item.Libro.Url
                });
            }

            return result;
        }
        #endregion
    }
}
