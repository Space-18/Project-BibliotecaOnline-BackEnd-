using Datos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs.With
{
    public class GuardadoDTOWithLibros : GuardadoDTO
    {
        public List<LibroDTO> Libros { get; set; }
    }
}
