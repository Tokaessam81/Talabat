using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;

namespace Talabat.Services
{
    //public class PaymentService : IPaymentServices
    //{
    //    private readonly IConfiguration _configuration;

    //    public PaymentService(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }
    //    public async Task<CustomerBasket> CreatePaymentIntent(int basketId)
    //    {
    //        //Get SecretKey
    //        StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];

    //    }
    //}
}
