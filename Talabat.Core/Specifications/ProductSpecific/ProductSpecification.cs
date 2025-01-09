using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecific
{
    public class ProductSpecification:BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParams spec) :base(
            P=>
            (string.IsNullOrEmpty(spec.SearchByName) || P.Name.ToLower().Contains(spec.SearchByName.ToLower()))&&
            (!spec.BrandId.HasValue || P.BrandId==spec.BrandId)&&
            (!spec.CatgoryId.HasValue||P.CategoryId==spec.CatgoryId))
            
        {
            Icludes.Add(p=>p.Brand);
            Icludes.Add(P=>P.Category);
            if(!string.IsNullOrEmpty(spec.sort) )
            {
                 switch(spec.sort)
                 {
                     case "priceAsc":
                        AddOrderBy(P => P.Price);
                            break;
                     case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                            break;
                    case "CategoryNameAsc":
                        AddOrderBy((P => P.Category.Name));
                        break;
                     default:
                        AddOrderBy(P => P.Name);
                            break;
                 };
            }
            else
               AddOrderBy(P => P.Name);

            if (spec.PageSize != 0 &&spec.PageIndex !=0)
            {
                AddPagination((spec.PageIndex - 1) * spec.PageSize, spec.PageSize); 
            }


        }
        public ProductSpecification(int Id):base(P=>P.Id==Id)
        {
            Icludes.Add(p => p.Brand);
            Icludes.Add(P => P.Category);
        }

    }
}
