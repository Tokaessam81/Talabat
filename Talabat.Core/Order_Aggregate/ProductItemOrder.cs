using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Order_Aggregate
{
    public class ProductItemOrder
    {
        public ProductItemOrder(int productId, string productName, string productURL)
        {
            ProductId = productId;
            ProductName = productName;
            ProductURL = productURL;
        }
        public ProductItemOrder()
        {
            
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductURL { get; set; }


    }
}
