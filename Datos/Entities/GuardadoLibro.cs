using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entities
{
    public  class GuardadoLibro
    {
        public int LibroId { get; set; }

        public int GuardadoId { get; set; }

        public int Orden { get; set; }

        public Libro Libro { get; set; }

        public Guardados Guardado { get; set; }
    }
}
