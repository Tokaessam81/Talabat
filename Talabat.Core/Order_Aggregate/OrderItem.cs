using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Order_Aggregate
{
    public class OrderItem:BaseEntity
    {
        public OrderItem(ProductItemOrder productItem, decimal price, int quantity)
        {
            this.productItem = productItem;
            Price = price;
            Quantity = quantity;
        }
        public OrderItem()
        {
            
        }
        public ProductItemOrder productItem { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
