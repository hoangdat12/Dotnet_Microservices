using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhileLagoon.Application.Feature.ShopAddressFeature.Command.AddShopAddress;
using WhileLagoon.Application.Feature.ShopAddressFeature.Command.CreateShopAddress;

namespace WhileLaggon.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ShopAddressController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("create")]
        public async Task<IActionResult> CreateShopAddress(CreateShopAddressCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddShopAddress(AddShopAddressCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
