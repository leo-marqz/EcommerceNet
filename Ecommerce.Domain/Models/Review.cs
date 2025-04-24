using Ecommerce.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public class Review : BaseModel
    {
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int ProductId { get; set; }
    }
}
