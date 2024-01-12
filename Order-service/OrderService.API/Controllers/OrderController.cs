
using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.Annotation;
using OrderService.Application.Dto;
using OrderService.Application.Dto.Order;
using OrderService.Application.Dto.OrderCheckout;
using OrderService.Application.Feature.OrderFeature.Command.CancelOrder;
using OrderService.Application.Feature.OrderFeature.Command.CheckoutProduct;
using OrderService.Application.Feature.OrderFeature.Command.CreateOrderCommand;
using OrderService.Application.Feature.OrderFeature.Query.GetOrderDetail;
using OrderService.Application.Feature.OrderFeature.Query.GetOrders;
using OrderService.Application.Feature.OrderFeature.Query.GetShopOrdersQuery;
using OrderService.Application.Response;
using OrderService.Domain.Entity;

namespace OrderService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController(
        IMediator mediator,
        IMapper mapper
    ): ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet("{orderId}")]
        [UserAuthorization]
        public async Task<Order> GetOrderDetail(Guid orderId)
        {
            ClaimsPrincipal principal = HttpContext.User;
            UserDecode user = _mapper.Map<UserDecode>(principal);

            GetOrderDetailQuery request = new()
            {
                User = user,
                OrderId = orderId
            };

            return await _mediator.Send(request);
        }

        [HttpGet()]
        [UserAuthorization]
        public async Task<List<Order>> GetOrders(int Page, int Limit)
        {
            ClaimsPrincipal principal = HttpContext.User;
            UserDecode user = _mapper.Map<UserDecode>(principal);

            GetOrdersQuery request = new()
            {
                User = user,
                Pagination = new Pagination()
                    {
                        Page = Page,
                        Limit = Limit
                    }
            };

            return await _mediator.Send(request);
        }

        [HttpGet("shop/{shopId}")]
        [UserAuthorization]
        public async Task<List<Order>> GetShopOrders(int Page, int Limit, Guid shopId)
        {
            ClaimsPrincipal principal = HttpContext.User;
            UserDecode user = _mapper.Map<UserDecode>(principal);

            GetShopOrderQuery request = new()
            {
                User = user,
                ShopId = shopId,
                Pagination = new Pagination()
                {
                    Page = Page,
                    Limit = Limit
                }
            };

            return await _mediator.Send(request);
        }

        [HttpPost("checkout")]
        public async Task<List<CheckoutProductRes>> Checkout(CheckoutProductCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        [UserAuthorization]
        public async Task<List<Order>> OrderProduct(List<CreateOrderReq> createOrdersReq)
        {
            ClaimsPrincipal principal = HttpContext.User;
            UserDecode user = _mapper.Map<UserDecode>(principal);

            CreateOrderCommand request = new()
            {
                User = user,
                CreateOrdersReq = createOrdersReq
            };

            return await _mediator.Send(request);
        }

        [HttpPost("cancel/{orderId}")]
        [UserAuthorization]
        public async Task<BaseResponse> CancelOrder(Guid orderId)
        {
            ClaimsPrincipal principal = HttpContext.User;
            UserDecode user = _mapper.Map<UserDecode>(principal);

            CancelOrderComand request = new()
            {
                User = user,
                OrderId = orderId
            };

            return await _mediator.Send(request);
        }
    }
}