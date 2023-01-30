using Datos.Entities;
using Negocio.DTOs;
using Negocio.DTOs.Add;
using Negocio.DTOs.With;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Services
{
    public interface ILibro
    {
        public Task<List<LibroDTO>> GetAll();

        public Task<LibroDTOWithAutorEditorialComentario> GetOne(int id);

        public Task<LibroDTO> Save(AddLibroDTO libroDTO);

        public Task<LibroDTO> Update(int id, AddLibroDTO libroDTO);

        public Task<int> Delete(int id);
    }
}
