

using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WhileLaggon.API.Annotation;
using WhileLagoon.Application.Dto.Cart;
using WhileLagoon.Application.Feature.CartFeature.Command.AddProduct;
using WhileLagoon.Application.Feature.CartFeature.Command.ChangeQuantity;
using WhileLagoon.Application.Feature.CartFeature.Command.CreateCart;
using WhileLagoon.Application.Feature.CartFeature.Query.GetCart;
using WhileLagoon.Domain.Entity;

namespace WhileLaggon.API.Controllers
{    
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CartController(IMediator mediator, IMapper mapper): Controller
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet("{userId}")]
        [UserAuthorization]
        public async Task<IActionResult> GetCart(Guid userId, int page = 1, int limit = 20)
        {
            ClaimsPrincipal principal = HttpContext.User;
            User user = _mapper.Map<User>(principal);

            return Ok(await _mediator.Send(request: new GetCartQuery(userId, user, page, limit)));
        }

        [HttpPost("{userId}")]
        [UserAuthorization]
         public async Task<IActionResult> AddProduct(Guid userId, AddProductToCart product)
        {
            ClaimsPrincipal principal = HttpContext.User;
            User user = _mapper.Map<User>(principal);

            AddProductCommand request = new()
            {
                Product = product,
                User = user
            };

            return Ok(await _mediator.Send(request));
        }

        [HttpPost("quantity/{userId}")]
        [UserAuthorization]
         public async Task<IActionResult> ChangeQuantity(Guid userId, AddProductToCart product)
        {
            ClaimsPrincipal principal = HttpContext.User;
            User user = _mapper.Map<User>(principal);

            ChangeQuantityCommand request = new()
            {
                Product = product,
                User = user
            };

            return Ok(await _mediator.Send(request));
        }

        [HttpPost()]
        public async Task<IActionResult> CreateCart(CreateCartCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}