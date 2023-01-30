using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs
{
    public class UserCredentials
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        //[RegularExpression(pattern: "^(?=.*[a-z]){3,}(?=.*[A-Z]){2,}(?=.*[0-9]){2,}(?=.*[!@#$%^&*()--__+.]){1,}.{8,16}$",
        //    ErrorMessage ="La contraseña debe tener entre 8 a 12 caracteres, los cuales incluyan una mayúscula, una minúscula, un número y un caracter especial")]
        public string Password { get; set; }
    }
}
