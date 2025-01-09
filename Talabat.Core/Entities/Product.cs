using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
        //Forign Key
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        //Navigation Property
       
        public ProductBrand Brand { get; set; }
        public ProductCategory Category { get; set; }

    }
}
