using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Completed")]
        Completed,

        [EnumMember(Value = "Shipped")]
        Shipped,

        [EnumMember(Value = "Cancelled")]
        Cancelled,

        [EnumMember(Value = "Error")]
        Error
    }
}
