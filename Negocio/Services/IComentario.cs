using Negocio.DTOs.Add;
using Negocio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Negocio.Services
{
    public interface IComentario
    {
        public Task<List<ComentarioDTO>> GetAll(int libroId);

        public Task<ComentarioDTO> GetOne(int id);

        public Task<ComentarioDTO> Save(int libroId, Claim claim, AddComentarioDTO comentarioDTO);

        public Task<int> Delete(int usuarioId, int id);
    }
}
