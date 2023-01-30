using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs.Add
{
    public class AddEditorialDTO
    {
        [Required]
        [RegularExpression("^(?!.* $)[A-Z][A-z ]+$"
            , ErrorMessage = " Ingrese un nombre válido.")]
        public string Nombre { get; set; }

        [Required]
        [RegularExpression("(?<s>^[\\D]+[ ])(?<h>[\\d]+)(?<e>.*?$)|",ErrorMessage = "Ingrese una dirección válida")]
        public string Direccion { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]{4,8}$", ErrorMessage = "Ingrese un teléfono válido")]
        public int Telefono { get; set; }
    }
}
