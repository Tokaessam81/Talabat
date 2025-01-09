using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregate;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.OrderSpecific;

namespace Talabat.Services
{
    public class OrderServices : IOrderServices
    {
        
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderServices(IBasketRepository basketRepo
           ,IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
           _unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateAsync(string BuyerEmail,string basketId, Address shippingAddress, int DeliveryMethodId)
        {
            //1.Get basket from basket repo
            var basket =await _basketRepo.GetBasketAsync(basketId);

            //2-get product from productrepo
            var orderItems=new List<OrderItem>();
            if (basket?.Items?.Count != null)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetById(item.Id);
                    var productItemOrder = new ProductItemOrder(item.Id,product.Name,product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrder, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }
            //3.calculate SubTotal
            var subTotal = orderItems.Sum(o => (o.Price) * o.Quantity);
            //GetDeliveryMethod from deliverymethodRepo
            var deliverymethod =await _unitOfWork.Repository<DeliveryMethod>().GetById(DeliveryMethodId);
            //create order
            var order=new Order(BuyerEmail,shippingAddress, deliverymethod, orderItems,subTotal);
            await _unitOfWork.Repository<Order>().CreatAsync(order);
            //sava to database
            var result=  await  _unitOfWork.completeAsync();

            //if pending,comp
            if (result <= 0)
            {
                return null!;
            }
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int id, string BuyerEmail)
        {
            var repo = _unitOfWork.Repository<Order>();
            var spec = new OrderSpecification(id, BuyerEmail);
            var order = await repo.GetByIdSpecification(spec);
            return order;

        }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync(string BuyerEmail)
        {
            var repo = _unitOfWork.Repository<Order>();
            var spec = new OrderSpecification(BuyerEmail);
            var order = await repo.GetAllBySpecificationAsync(spec);
            return order;
        }
    }
}
