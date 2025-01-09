using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using TalabatAPI.DTO;
using TalabatAPI.Errors;

namespace TalabatAPI.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string Id)
        {
            var basket= await _basketRepository.GetBasketAsync(Id);
            return Ok(basket??new CustomerBasket(Id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket)
        {
            var BasketDTO=_mapper.Map<CustomerBasketDTO,CustomerBasket>(basket);
            var CreatOrUpdateBasket =await _basketRepository.UpdateBasketAsync(BasketDTO);
            if (CreatOrUpdateBasket is null)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));
            return Ok(CreatOrUpdateBasket);
        }
        [HttpDelete("{Id}")]
        public async Task DeleteBasket(string Id)
        {
            await _basketRepository.DeleteBasketAsync(Id);
        }

    }
}
