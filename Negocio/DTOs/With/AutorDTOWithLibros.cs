using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs.With
{
    public class AutorDTOWithLibros : AutorDTO
    {
        public List<LibroDTO> Libros { get; set; }
    }
}
