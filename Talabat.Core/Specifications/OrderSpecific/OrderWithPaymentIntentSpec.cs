using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregate;

namespace Talabat.Core.Specifications.OrderSpecific
{
    public class OrderWithPaymentIntentSpec : BaseSpecification<Order>
    {
        public OrderWithPaymentIntentSpec(string PaymentintentId) :
           base(o => o.PaymentInitId == PaymentintentId)
        { 
        }
        }
}
