using E_Commerce.Core.Entities;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Core.Specifications.Specification.Interfaces;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.GetQuery;
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
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
           return await SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>() , spec).AsNoTracking().ToListAsync();
        }
        public async Task<T?> GetSpecAsync(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplaySpecification(ISpecification<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }
    }
}
