using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entities
{
    public class EditorialLibro
    {
        public int LibroId { get; set; }

        public int EditorialId { get; set; }

        public int Orden { get; set; }

        public Libro Libro { get; set; }

        public Editorial Editorial { get; set; }
    }
}
