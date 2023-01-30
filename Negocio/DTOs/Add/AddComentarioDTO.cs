using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs.Add
{
    public class AddComentarioDTO
    {
        [Required]
        public string Texto { get; set; }
    }
}
