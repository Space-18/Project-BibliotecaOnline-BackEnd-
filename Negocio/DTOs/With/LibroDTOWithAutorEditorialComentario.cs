using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs.With
{
    public class LibroDTOWithAutorEditorialComentario : LibroDTO
    {
        public List<AutorDTO> Autores { get; set; }

        public List<EditorialDTO> Editoriales { get; set; }

        public List<ComentarioDTO> Comentarios { get; set; }
    }
}
