
using Ecommerce.Application.Persistence;
using Ecommerce.Infrastructure.Persistence;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable? _repositories;
        private readonly EcommerceApplicationDbContext _context;

        public UnitOfWork(EcommerceApplicationDbContext context)
        {
            _context = context;   
        }

        public async Task<int> ApplyChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la transaccion (uow): ", ex);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepositoryAsync<TEntity> Repository<TEntity>() where TEntity : class
        {
            if(_repositories is null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepositoryAsync<>);
                var repositoryInstance = Activator.CreateInstance(
                        type: repositoryType.MakeGenericType( typeof(TEntity) ), 
                        _context
                    );
                _repositories.Add( key: type, value: repositoryInstance );
            }
            return (IRepositoryAsync<TEntity>)_repositories[type]!;
        }
    }
}
