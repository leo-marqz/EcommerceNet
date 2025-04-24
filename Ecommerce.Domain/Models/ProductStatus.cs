using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models
{
    public enum ProductStatus
    {
        [EnumMember(Value = "Inactive")]
        Inactive = 0,

        [EnumMember(Value = "Active")]
        Active = 1
    }
}
