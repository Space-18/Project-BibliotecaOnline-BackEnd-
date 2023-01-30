using Negocio.DTOs;
using Negocio.DTOs.Add;
using Negocio.DTOs.With;
using Negocio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Services
{
    public interface IEditorial
    {
        public Task<List<EditorialDTO>> GetAll();

        public Task<EditorialDTOWithLibros> GetOne(int id);

        public Task<EditorialDTO> Save(AddEditorialDTO editorialDTO);

        public Task<EditorialDTO> Update(int id, AddEditorialDTO editorialDTO);

        public Task<int> Delete(int id);
    }
}
