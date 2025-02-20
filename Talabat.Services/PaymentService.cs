using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregate;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Services.Contract;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Services
{
    public class PaymentService : IPaymentServices
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basket;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration,IBasketRepository basket,IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basket = basket;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreatePaymentIntent(string basketId)
        {
            //Get SecretKey
            StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];
            //GetBasket 

            var basket = await _basket.GetBasketAsync(basketId);
            if (basket == null) return null!;
            if (basket.Items.Count>0)
            {
                foreach (var product in basket.Items)
                {
                     var Product = await _unitOfWork.Repository<Product>().GetById(product.Id);
                    if (product.Price != Product.Price)
                    {
                        product.Price = Product.Price;
                    }
                }
            }
            var shippingCost = 0M;
            if (basket.DeliveryMethodId != null)
            {
                var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetById(basket.DeliveryMethodId);
                shippingCost=DeliveryMethod.Cost;
            }
            var subTotal=basket.Items.Sum(ITEM=>ITEM.Price*ITEM.Quantity);
            var services = new PaymentIntentService();
            PaymentIntent paymentintent;
            if (string.IsNullOrEmpty(basket.PaymentIntetId))
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(shippingCost * 100 + subTotal * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                paymentintent = await services.CreateAsync(option);
                basket.PaymentIntetId = paymentintent.Id;
                basket.ClientSecret = paymentintent.ClientSecret;

            }
            else 
            {
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(shippingCost * 100 + subTotal * 100)
                };
                paymentintent=await services.UpdateAsync(basket.PaymentIntetId, option);
                basket.PaymentIntetId = paymentintent.Id;
                basket.ClientSecret = paymentintent.ClientSecret;
            }
            await _basket.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
