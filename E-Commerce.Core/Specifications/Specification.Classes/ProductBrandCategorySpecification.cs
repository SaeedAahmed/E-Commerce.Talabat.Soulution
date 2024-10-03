using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Specification.Classes
{
    public class ProductBrandCategorySpecification : BaseSpecification<Product>
    {
        public ProductBrandCategorySpecification():base()
        {
            AddInclude();
        }
        public ProductBrandCategorySpecification(int id) : base(P=>P.Id == id)
        {
            AddInclude();
        }

        private void AddInclude()
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
