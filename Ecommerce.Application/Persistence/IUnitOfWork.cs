using System;
using System.Threading.Tasks;

namespace Ecommerce.Application.Persistence
{
    //Its main mission: to coordinate multiple database operations
    //so that they are all stored together(or not at all, if something goes wrong).
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryAsync<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> ApplyChangesAsync();
    }
}
