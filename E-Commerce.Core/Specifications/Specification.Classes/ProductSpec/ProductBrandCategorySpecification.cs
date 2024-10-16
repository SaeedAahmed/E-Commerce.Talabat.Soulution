using E_Commerce.Core.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Specification.Classes.ProductSpec
{
    public class ProductBrandCategorySpecification : BaseSpecification<Product>
    {
        public ProductBrandCategorySpecification(ProductSpecificationParameters spec)
            : base(ProductSpecificationsHelper.GetCriteria(spec))
        {
            AddInclude();
            if (!string.IsNullOrEmpty(spec.Sort))
            {
                switch (spec.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;

                    case "PriceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }
            ApplyPagination((spec.PageIndex - 1) * spec.PageSize, spec.PageSize);
        }
        public ProductBrandCategorySpecification(int id) : base(P => P.Id == id)
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
