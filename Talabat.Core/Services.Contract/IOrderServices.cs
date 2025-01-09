using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregate;

namespace Talabat.Core.Services.Contract
{
    public interface IOrderServices
    {
        Task<Order> CreateAsync(string BuyerEmail,string basketId, Address shippingAddress, int DeliveryMethodId);
        Task<IReadOnlyList<Order>> GetOrdersAsync(string BuyerEmail);
        Task<Order> GetOrderByIdAsync(int id ,string BuyerEmail);
    }
}
