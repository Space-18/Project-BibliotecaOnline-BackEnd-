using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs.Add
{
    public class AddAutorDTO
    {
        [Required]
        [RegularExpression("^(?!.* $)[A-Z][A-z ]+$"
            , ErrorMessage =" Ingrese un nombre válido.")]
        public string Nombres { get; set; }

        [Required]
        [RegularExpression("^(?!.* $)[A-Z][A-z ]+$"
            , ErrorMessage = " Ingrese un apellido válido.")]
        public string Apellidos { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]{0,1}$",ErrorMessage ="Ingrese una edad válida")]
        public int Edad { get; set; }

        [Required]
        [RegularExpression("^(?!.* $)[A-Z][A-z ]+$"
            , ErrorMessage = " Ingrese una nacionalidad válida.")]
        public string Nacionalidad { get; set; }
    }
}
