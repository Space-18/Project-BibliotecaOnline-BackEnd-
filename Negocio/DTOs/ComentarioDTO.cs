using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs
{
    public class ComentarioDTO
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public string Texto { get; set; }
    }
}
