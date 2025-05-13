
using Ecommerce.Domain.Models;
using System.Collections.Generic;

namespace Ecommerce.Application.Contracts.Identitity
{
    public interface IAuthService
    {
        string GetSessionUser();
        string CreateToken(User user, IList<string>? roles = null);
    }
}
