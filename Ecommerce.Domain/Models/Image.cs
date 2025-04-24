
using Ecommerce.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    internal class Image : BaseModel
    {
        public int ProductId { get; set; }

        [Column(TypeName = "NVARCHAR(4000)")]
        public string Url { get; set; } = string.Empty;
        public string PublicCode { get; set; } = string.Empty;
    }
}
