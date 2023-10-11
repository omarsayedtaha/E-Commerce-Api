using AutoMapper;
using CoreLayer.Entities.Order_Agregate;
using CoreLayer.Repository;
using CoreLayer.Services;
using CoreLayer.Specification;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;
using System.Security.Claims;
using TalabatApi.Dtos;
using TalabatApi.Errors;

namespace TalabatApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork
            ,IOrderService orderService
            ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderdto )
        {
            var buyeremail = User.FindFirstValue(ClaimTypes.Email);
            var Address = _mapper.Map<AddressDto ,Address>(orderdto.ShippingAddress);
            
            var order = await _orderService.CreateOrderAsync(buyeremail, orderdto.BasketId, orderdto.DeliverMethodId, Address) ;

            if (order is null) return BadRequest(new ApiErrorResponse(400));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrderForUser()
        {
            var buyeEmail = User.FindFirstValue(ClaimTypes.Email);

            var spec = new OrderSpecification(buyeEmail);

            var orders = _unitOfWork.Repository<Order>();
            if (orders is not null)
               await orders.GetAllWihtSpecAsync(spec);
            else
                return BadRequest(new ApiErrorResponse(400));

            return Ok(orders); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderForUserById(int orderId)
        {
            var buyeEmail = User.FindFirstValue(ClaimTypes.Email);

            var spec = new OrderSpecification(buyeEmail, orderId);

            var orders = _unitOfWork.Repository<Order>();
            if (orders is not null)
                await orders.GetByIdWihtSpecAsync(spec);
            else
                return BadRequest(new ApiErrorResponse(400));

            return Ok(orders);
        }
    }
}
