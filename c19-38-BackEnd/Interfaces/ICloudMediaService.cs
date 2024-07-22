using CloudinaryDotNet.Actions;

namespace c19_38_BackEnd.Interfaces
{
    public interface ICloudMediaService
    {
        Task<string?> SubirVideoPost(IFormFile file);
        Task<string?> SubirFotoPost(IFormFile file);
        Task<string?> SubirFotoPerfil(IFormFile file);
    }
}
