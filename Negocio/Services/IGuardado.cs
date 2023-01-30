using Negocio.DTOs;
using Negocio.DTOs.Add;
using Negocio.DTOs.With;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Services
{
    public interface IGuardado
    {
        public Task<List<GuardadoDTO>> GetAll(Claim claim);

        public Task<GuardadoDTOWithLibros> GetOne(Claim claim, int id);

        public Task<GuardadoDTO> Save(Claim claim, AddGuardadoDTO guardadoDTO);

        public Task<GuardadoDTO> Update(Claim claim, int id, AddGuardadoDTO guardadoDTO);

        public Task<int> Delete(Claim claim, int id);
    }
}
