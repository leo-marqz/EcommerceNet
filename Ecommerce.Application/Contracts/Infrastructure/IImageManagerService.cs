
using Ecommerce.Application.Models.ImageManager;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts.Infrastructure
{
    public interface IImageManagerService
    {
        Task<ImageManagerResponse> UploadImageAsync(ImageStream imageStream);
    }
}
