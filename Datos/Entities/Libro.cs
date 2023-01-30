using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entities
{
    public class Libro
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Portada { get; set; }

        public string Url { get; set; }

        public List<AutorLibro> AutorLibros { get; set; }

        public List<EditorialLibro> EditorialLibros { get; set; }

        public List<Comentario> Comentarios { get; set; }

        public List<GuardadoLibro> GuardadoLibros { get; set; }
    }
}
