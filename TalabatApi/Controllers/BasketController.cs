using AutoMapper;
using CoreLayer.Entities.Basket_Module;
using CoreLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using TalabatApi.Dtos;
using TalabatApi.Errors;

namespace TalabatApi.Controllers
{
   
    public class BasketController :ApiBaseController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepo , IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }

        [HttpGet]
         public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
         {
            var basket = await _basketRepo.GetBasketAsync(id);
            if (basket is null) new CustomerBasket(id);
            return basket;
         }


        [HttpPost]

        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto customerBasket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasket);
            var basket = await _basketRepo.UpdateBasketAsync(mappedBasket);
          
            if (basket is null) return  BadRequest(new ApiErrorResponse(400));
            return mappedBasket;
        }


        [HttpDelete]
        public Task<bool> DeleteBasket(string id)
        {
            return _basketRepo.DeleteBasketAsync(id);
        }
    }
}
