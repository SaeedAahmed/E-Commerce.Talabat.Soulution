using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Specification.Classes
{
    public class ProductSpecificationParameters
    {
        private int _PageSize = 5;
        private const int MaxPageSize = 10;

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value> MaxPageSize? MaxPageSize:value; }
        }
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public string? Search { get; set; }


    }
}
