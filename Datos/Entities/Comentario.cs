using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entities
{
    public class Comentario
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public string Texto { get; set; }

        public int LibroId { get; set; }

        public Libro Libro { get; set; }

        public string UsuarioId { get; set; }

        public IdentityUser Usuario { get; set; }
    }
}
