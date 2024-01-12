using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WhileLaggon.API.Annotation;
using WhileLagoon.Application.Dto.Inventory;
using WhileLagoon.Application.Feature.InventoryFeature.Command.ChangeQuantity;
using WhileLagoon.Application.Feature.InventoryFeature.Query.ViewInventory;
using WhileLagoon.Domain.Entity;

namespace WhileLaggon.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class InventoryController(
        ILogger<InventoryController> logger,
        IMediator mediator,
        IMapper mapper
    ) : Controller
    {
        private readonly ILogger<InventoryController> _logger = logger;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpPut]
        [UserAuthorization]
        public async Task<IActionResult> UpdateInventory(UpdateInventoryReq data) {
            ClaimsPrincipal principal = HttpContext.User;
            User user = _mapper.Map<User>(principal);
            
            UpdateInventoryCommand request = new()
            {
                User = user,
                Req = data
            };
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{productId}")]
        [UserAuthorization]
        public async Task<IActionResult> GetInventory(Guid productId) {
            ClaimsPrincipal principal = HttpContext.User;
            User user = _mapper.Map<User>(principal);
            
            ViewInventoryQuery request = new()
            {
                User = user,
                ProductId = productId
            };
            return Ok(await _mediator.Send(request));
        }
    }
}