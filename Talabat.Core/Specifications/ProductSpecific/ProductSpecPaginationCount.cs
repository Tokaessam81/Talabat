using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecific
{
    public class ProductSpecPaginationCount:BaseSpecification<Product>
    {
        public ProductSpecPaginationCount(ProductSpecParams spec) : base(
            P =>
            (string.IsNullOrEmpty(spec.SearchByName) || P.Name.ToLower().Contains(spec.SearchByName.ToLower())) &&
            (!spec.BrandId.HasValue || P.BrandId == spec.BrandId) &&
            (!spec.CatgoryId.HasValue || P.CategoryId == spec.CatgoryId)
            )
        {
                
        }

    }
}
