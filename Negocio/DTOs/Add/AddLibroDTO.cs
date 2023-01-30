using Microsoft.AspNetCore.Http;
using Negocio.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs.Add
{
    public class AddLibroDTO
    {
        [Required]
        public string Nombre { get; set; }

        [PesoValidation(maxMB: 5)]
        [TypeValidation(fileTypeGroup: FileTypeGroup.Imagen)]
        public IFormFile Portada { get; set; }

        [PesoValidation(maxMB: 30)]
        [TypeValidation(fileTypeGroup: FileTypeGroup.Archivo)]
        public IFormFile Url { get; set; }

        public List<int> AutorId { get; set; }

        public List<int> EditorialId { get; set; }
    }
}
