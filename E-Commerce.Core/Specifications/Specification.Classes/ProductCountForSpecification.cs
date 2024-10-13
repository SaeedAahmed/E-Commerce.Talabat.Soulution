using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Specification.Classes
{
    public class ProductCountForSpecification : BaseSpecification<Product>
    {
        public ProductCountForSpecification(ProductSpecificationParameters spec)
            :base(p=>
            (string.IsNullOrEmpty(spec.Search) || p.Name.ToLower().Contains(spec.Search.ToLower())) &&
              (!spec.BrandId.HasValue ||p.BrandId == spec.BrandId.Value )&&
              (!spec.CategoryId.HasValue || p.TypeId == spec.CategoryId.Value) 

            )
        {
            
        }
    }
}
