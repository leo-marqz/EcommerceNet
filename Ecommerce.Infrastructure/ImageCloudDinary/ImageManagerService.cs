
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.ImageManager;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.ImageCloudDinary
{
    public class ImageManagerService : IImageManagerService
    {
        private readonly Cloudinary _cloudinary;

        public ImageManagerService(IOptions<CloudinarySettings> cloudinarySettings)
        {
            var settings = cloudinarySettings.Value;
            var account = new Account(settings.APIKeyName, settings.APIKey, settings.APISecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageManagerResponse> UploadImageAsync(ImageStream imageStream)
        {
            var parameters = new ImageUploadParams();
            parameters.File = new FileDescription(imageStream.Filename, imageStream.Stream);

            var result = await _cloudinary.UploadAsync(parameters);

            if(result.StatusCode == HttpStatusCode.OK)
            {
                return new ImageManagerResponse
                {
                    Url = result.Url.ToString(),
                    PublicId = result.PublicId
                };
            }
            throw new Exception($"Image upload failed: {result.Error?.Message}");
        }
    }
}
