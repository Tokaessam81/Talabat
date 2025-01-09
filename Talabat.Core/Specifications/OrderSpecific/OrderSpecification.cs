using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregate;

namespace Talabat.Core.Specifications.OrderSpecific
{
    public class OrderSpecification:BaseSpecification<Order>
    {
        public OrderSpecification(string BuyerEmail):
            base(o=>o.BuyerEmail==BuyerEmail)
        {
            Icludes.Add(o=>o.shippingAddress);
            Icludes.Add(o=>o.deliveryMethod);
            Icludes.Add(o=>o.OrderItems);
            AddOrderBy(o => o.OrderDate);
        }  
        public OrderSpecification(int Id,string BuyerEmail):
            base(o=>o.Id==Id && o.BuyerEmail==BuyerEmail)
        {
            Icludes.Add(o=>o.shippingAddress);
            Icludes.Add(o=>o.deliveryMethod);
            Icludes.Add(o=>o.OrderItems);
           
        }
    }
}
