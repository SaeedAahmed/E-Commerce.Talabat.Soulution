using E_Commerce.Core.Entities;
using E_Commerce.Core.Specifications.Specification.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.GetQuery
{
    public static class SpecificationsEvaluator<T> where T : BaseEntity 
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery , ISpecification<T> spec)
        {
            var query = inputQuery; //_dbContext.Set<Product>()

            if (spec.Criterial is not null)
            {
                query = query.Where(spec.Criterial);//_dbContext.Set<Product>().Where(P=>P.Id==id)
            }
            if(spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);//_dbContext.Set<Product>().Where(P=>P.Id==id).OrderBy(P=>P.price)
            }
            else if (spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);//_dbContext.Set<Product>().Where(P=>P.Id==id).OrderByDesc(P=>P.price)
            }

            if(spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query,(CurrentQuery , includeExpression) => CurrentQuery.Include(includeExpression));
                                       //_dbContext.Set<Product>().Include(P => P.ProductBrand).Include(P => P.ProductType)
            return query;
        }
    }
}
