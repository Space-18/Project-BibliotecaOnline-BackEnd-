using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Negocio.Services;
using System.Security.Policy;

namespace Negocio.Class
{
    public class FileStorageClass : IFileStorage
    {
        private readonly string connectionString;

        public FileStorageClass(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("azureStorageKey");
        }

        public async Task<string> SaveFile(byte[] contenido, string extension, string contenedor, string contentType)
        {
            var cliente = new BlobContainerClient(connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            cliente.SetAccessPolicy(PublicAccessType.Blob);

            var archivonombre = $"{Guid.NewGuid()}{extension}";
            var blob = cliente.GetBlobClient(archivonombre);

            var blobUploadOptions = new BlobUploadOptions();
            var blobHttpHeader = new BlobHttpHeaders();

            blobHttpHeader.ContentType = contentType;
            blobUploadOptions.HttpHeaders = blobHttpHeader;

            await blob.UploadAsync(new BinaryData(contenido),blobUploadOptions);

            return blob.Uri.ToString();
        }

        public async Task<string> UpdateFile(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            await DeleteFile(ruta, contenedor);
            return await SaveFile(contenido,extension,contenedor,contentType);
        }

        public async Task DeleteFile(string ruta, string contenedor)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                return;
            }

            var cliente = new BlobContainerClient(connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();

            var archivo = Path.GetFileName(ruta);
            var blob = cliente.GetBlobClient(archivo);

            await blob.DeleteIfExistsAsync();
        }
    }
}
