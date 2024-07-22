using c19_38_BackEnd.Configuracion;
using c19_38_BackEnd.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace c19_38_BackEnd.Servicios
{
    public class CloudMediaService : ICloudMediaService
    {
        private readonly Cloudinary _cloudinary;
        public CloudMediaService(CloudinarySettings cloudinarySettings)
        {
            var cuentaCloud = new Account(cloudinarySettings.Cloud, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret);
            _cloudinary = new Cloudinary(cuentaCloud);
        }

        /// <summary>
        /// Sube la foto del perfil de un suario con el tamaño adecuado a la nube
        /// </summary>
        /// <param name="file">archivo a subir que implementa la interfaz IFormFile</param>
        /// <returns>Dirrecion de la imagen para ser accedida en formato string Url, devuelve null si la foto no se subio correctamente o surgio un error</returns>
        public async Task<string?> SubirFotoPerfil(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = ConfigureImageTransformation(new Transformation().Height(250).Width(250).Crop("scale"), stream, file.FileName);
            return await SubirMedia(uploadParams);
        }

        /// <summary>
        /// Sube la foto de un post con el tamaño adecuado a la nube
        /// </summary>
        /// <param name="file">archivo a subir que implementa la interfaz IFormFile</param>
        /// <returns>Dirrecion de la imagen para ser accedida en formato string Url, devuelve null si la foto no se subio correctamente o surgio un error</returns>
        public async Task<string?> SubirFotoPost(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = ConfigureImageTransformation(new Transformation().Height(1.0).Width(1.0).Crop("scale"), stream, file.FileName);
            return await SubirMedia(uploadParams);
        }
        /// <summary>
        /// Sube el video de un post a la nube
        /// </summary>
        /// <param name="file">archivo a subir que implementa la interfaz IFormFile</param>
        /// <returns>Dirrecion del video para ser accedida en formato string Url, devuelve null si la foto no se subio correctamente o surgio un error</returns>
        public async Task<string?> SubirVideoPost(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = ConfigureVideoTransformation(new Transformation().Width(1.0).Height(1.0).Crop("scale"), stream, file.FileName);
            return await SubirMedia(uploadParams);
        }

        public async Task<string?> SubirMedia(RawUploadParams uploadParams)
        {
            try
            {
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.Url.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ImageUploadParams ConfigureImageTransformation(Transformation transformation, Stream stream, string name)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription
                {
                    FileName = name,
                    Stream = stream
                },
                Transformation = transformation
            };
            return uploadParams;
        }

        public VideoUploadParams ConfigureVideoTransformation(Transformation transformation, Stream stream, string name)
        {
            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription
                {
                    FileName = name,
                    Stream = stream,
                },
                Transformation = transformation
            };
            return uploadParams;
        }
    }
}
