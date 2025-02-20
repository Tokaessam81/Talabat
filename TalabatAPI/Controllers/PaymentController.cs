using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;
using TalabatAPI.DTO;
using TalabatAPI.Errors;
using TalabatAPI.Helpers;

namespace TalabatAPI.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentServices _paymentServices;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentServices paymentServices,IMapper mapper)
        {
            _paymentServices = paymentServices;
            _mapper = mapper;
        }
        [CacheAttribute(300)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePayment(string basketId)
        {
            var basket = await _paymentServices.CreatePaymentIntent(basketId);
            if (basket == null) return BadRequest(new ApiResponse(400));
            var basketdto=_mapper.Map<CustomerBasket,CustomerBasketDTO>(basket);
            return Ok(basketdto);
        }
    }
}
