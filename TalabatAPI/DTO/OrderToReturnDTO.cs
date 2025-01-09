using Talabat.Core.Order_Aggregate;

namespace TalabatAPI.DTO
{
    public class OrderToReturnDTO
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } 

        public string Status { get; set; }
        public Address shippingAddress { get; set; }
        public string deliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = new HashSet<OrderItemDto>();
        public decimal subTotal { get; set; }
        public decimal Total { get; set; }
        public string? PaymentInitId { get; set; }=string.Empty;
    }
}
