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
    public interface IAutor
    {
        public Task<List<AutorDTO>> GetAll();

        public Task<AutorDTOWithLibros> GetOne(int id);

        public Task<AutorDTO> Save(AddAutorDTO autorDTO);

        public Task<AutorDTO> Update(int id, AddAutorDTO autorDTO);

        public Task<int> Delete(int id);
    }
}
