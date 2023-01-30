using Datos.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs.Add
{
    public class AddGuardadoDTO
    {
        [Required]
        public string Nombre { get; set; }

        public List<int> LibrosId { get; set; }
    }
}
