using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Order_Aggregate
{
    public class Order:BaseEntity
    {

        public Order()
        {
        }

        public Order(string buyerEmail, Address address, DeliveryMethod deliveryMethod, ICollection<OrderItem> orderItems, decimal subTotal, string? paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            this.shippingAddress = address;
            this.deliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            this.subTotal = subTotal;
            PaymentInitId = paymentIntentId;
        }
        
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.UtcNow;

        public OrderStatus Status { get; set; }
        public Address shippingAddress { get; set; }
        public int? DeliveryMethodId { get; set; }
        public DeliveryMethod deliveryMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }= new HashSet<OrderItem>();
        public decimal subTotal { get; set; }
        public decimal GetTotal()=>subTotal+deliveryMethod.Cost;
        public string? PaymentInitId { get; set; }

    }
}
