using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Order_Aggregate;
using Talabat.Core.Services.Contract;
using TalabatAPI.DTO;
using TalabatAPI.Errors;

namespace TalabatAPI.Controllers
{
  
    public class OrdersController : BaseController
    {
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;

        public OrdersController(IOrderServices orderServices,IMapper mapper)
        {
            _orderServices = orderServices;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO order)
        {
            var address =  _mapper.Map<AddressDTO,Address>(order.shippingAddress);
            var result=  await  _orderServices.CreateAsync(order.BuyerEmail, order.basketId, address, order.DeliveryMethodId);
            if (result is null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order,OrderToReturnDTO>(result));

        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrdersAsync(string Email)
        {
            
            var order=  await _orderServices.GetOrdersAsync(Email);
            if (order is null) 
            return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map< IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDTO>>(order));

        } 
        [HttpGet("{Id}")]
        public async Task<ActionResult<Order>> GetOrderAsync(int Id,string Email)
        {
             var order=  await _orderServices.GetOrderByIdAsync(Id,Email);
             if (order is null) 
             return BadRequest(new ApiResponse(400));
             return Ok(_mapper.Map<Order, OrderToReturnDTO>(order));

        }

    }
}
