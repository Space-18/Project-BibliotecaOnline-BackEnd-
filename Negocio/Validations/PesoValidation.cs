using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Validations
{
    public class PesoValidation : ValidationAttribute
    {
        private readonly int MaxMB;

        public PesoValidation(int maxMB)
        {
            MaxMB = maxMB;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null) { return ValidationResult.Success; }

            IFormFile formFile = value as IFormFile;

            if(formFile == null) { return ValidationResult.Success; }

            if (formFile.Length > (MaxMB * 1024) * 1024) {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {MaxMB} MB.");
            }

            return ValidationResult.Success;
        }
    }
}
