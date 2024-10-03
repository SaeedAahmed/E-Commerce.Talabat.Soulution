﻿using E_Commerce.Core.Entities;
using E_Commerce.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetSpecAsync(ISpecification<T> spec);
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec);

    }
}
