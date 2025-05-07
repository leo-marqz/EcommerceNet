
using System.IO;

namespace Ecommerce.Application.Models.ImageManager
{
    public class ImageStream
    {
        public Stream? Stream { get; set; }
        public string? Filename { get; set; }
        public string? Extension { get; set; }
    }
}
