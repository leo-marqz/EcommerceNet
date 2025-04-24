using System;

namespace Ecommerce.Domain.Common
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
        public DateTime LastModifiedAt { get; set; }
    }
}
