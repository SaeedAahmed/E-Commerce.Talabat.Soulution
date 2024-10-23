using E_Commerce.Core.Entities;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private readonly Hashtable _Repositories;
        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _Repositories = new Hashtable();
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!_Repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _Repositories.Add(type, repository);
            }
            return _Repositories[type] as IGenericRepository<TEntity>;
        }
        public async Task<int> Complete()
        {
          return  await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

       
    }
}
