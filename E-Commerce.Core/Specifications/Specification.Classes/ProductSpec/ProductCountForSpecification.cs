using E_Commerce.Core.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Specification.Classes.ProductSpec
{
    public class ProductCountForSpecification : BaseSpecification<Product>
    {
        public ProductCountForSpecification(ProductSpecificationParameters spec)
            : base(ProductSpecificationsHelper.GetCriteria(spec))
        {

        }
    }
}
