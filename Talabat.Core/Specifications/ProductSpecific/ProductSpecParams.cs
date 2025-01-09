using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.ProductSpecific
{
    public class ProductSpecParams
    {
        public string? sort { get; set; }
        public int? BrandId { get; set; }
        public int? CatgoryId { get; set; }

        private const int MaxPagesize=10;
        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPagesize ? MaxPagesize : value; }
        }

        public int PageIndex { get; set; }
        public string? SearchByName { get; set; }
    }
}
