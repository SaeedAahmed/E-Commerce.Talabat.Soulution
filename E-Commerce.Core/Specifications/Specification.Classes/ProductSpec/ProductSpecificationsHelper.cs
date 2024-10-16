using E_Commerce.Core.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Specification.Classes.ProductSpec
{
    public static class ProductSpecificationsHelper
    {
        public static Expression<Func<Product, bool>> GetCriteria(ProductSpecificationParameters spec)
        {
            return p =>
                (string.IsNullOrEmpty(spec.Search) || p.Name.ToLower().Contains(spec.Search.ToLower())) &&
                (!spec.BrandId.HasValue || p.BrandId == spec.BrandId) &&
                (!spec.TypeId.HasValue || p.TypeId == spec.TypeId);
        }
    }
}
