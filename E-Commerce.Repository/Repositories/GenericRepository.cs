using E_Commerce.Core.Entities;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>) await _dbContext.Set<Product>().Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();
            }
            return await _dbContext.Set<T>().ToListAsync();
        }
       
        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await _dbContext.Set<Product>().Where(P=>P.Id==id).Include(P=>P.ProductBrand).Include(P=>P.ProductType).FirstOrDefaultAsync() as T;
            }
            return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}
