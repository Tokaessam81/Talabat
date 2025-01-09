using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket(string cusomerId)
        {
           Id=cusomerId;
            Items = new List<BasketItem>();
        }
        public CustomerBasket()
        {
            Items = new List<BasketItem>();
        }
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
