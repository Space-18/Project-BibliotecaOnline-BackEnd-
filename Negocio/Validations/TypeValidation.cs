using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Validations
{
    public class TypeValidation :ValidationAttribute
    {
        private readonly string[] TypeValid;

        public TypeValidation(string[] typeValid)
        {
            TypeValid = typeValid;
        }

        public TypeValidation(FileTypeGroup fileTypeGroup)
        {
            if (fileTypeGroup == FileTypeGroup.Imagen)
            {
                TypeValid = new string[] { "image/jpg", "image/jpeg", "image/png" };
            }else if (fileTypeGroup == FileTypeGroup.Archivo)
            {
                TypeValid = new string[] { "application/pdf" };
            }
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) { return ValidationResult.Success; }

            IFormFile formFile = value as IFormFile;

            if (formFile == null) { return ValidationResult.Success; }

            if (!TypeValid.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo de archivo debe ser uno de los siguiente: {string.Join(", ",TypeValid)}");
            }

            return ValidationResult.Success;
        }
    }
}
