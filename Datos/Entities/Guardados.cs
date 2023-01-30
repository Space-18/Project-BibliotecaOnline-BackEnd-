using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entities
{
    public class Guardados
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string UsuarioId { get; set; }

        public IdentityUser Usuario { get; set; }

        public bool Estado { get; set; }

        public List<GuardadoLibro> GuardadoLibros { get; set; }
    }
}
