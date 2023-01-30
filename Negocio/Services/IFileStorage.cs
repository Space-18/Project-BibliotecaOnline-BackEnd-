using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Services
{
    public interface IFileStorage
    {
        Task<string> SaveFile(byte[] contenido, string extension, string contenedor, string contentType);

        Task<string> UpdateFile(byte[] contenido, string extension, string contenedor, string ruta, string contentType);

        Task DeleteFile(string ruta, string contenedor);
    }
}
