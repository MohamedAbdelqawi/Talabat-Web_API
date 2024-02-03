using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Apis.Errors;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;

namespace Talabat.Apis.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            return basket ?? new CustomerBasket(basketId);

        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedbasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);

            var updateorcreate = await _basketRepository.UpdateBasketAsync(mappedbasket);
            if (updateorcreate is null) return BadRequest(new ApiResponse(400));
            return updateorcreate;
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string basketid)
        {
            return await _basketRepository.DeleteBasketAsync(basketid);
        }
    }
}
