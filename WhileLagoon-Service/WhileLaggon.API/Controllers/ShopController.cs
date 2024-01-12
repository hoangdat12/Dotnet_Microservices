using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using WhileLaggon.API.Annotation;
using WhileLagoon.Application.Dto.Shop;
using WhileLagoon.Application.Feature.ShopFeature.Command.CreateShop;
using WhileLagoon.Application.Feature.ShopFeature.Command.UpdateShopInformation;
using WhileLagoon.Application.Feature.ShopFeature.Command.UploadShopAvatar;
using WhileLagoon.Domain.Entity;

namespace WhileLaggon.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ShopController(IMediator mediator, IMapper mapper) : Controller
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> CreateShop(CreateShopReq createShopReq)
        {
            ClaimsPrincipal principal = HttpContext.User;
            User user = _mapper.Map<User>(principal);

            CreateShopCommand request = new()
            {
                User = user,
                CreateShopReq = createShopReq
            };

            return Ok(await _mediator.Send(request));
        }

        [HttpPatch]
        [UserAuthorization]
        public async Task<IActionResult> UpdateShop(UpdateShopInfor updateShopInfor)
        {
            ClaimsPrincipal principal = HttpContext.User;
            User user = _mapper.Map<User>(principal);

            UpdateShopInformationCommand request = new()
            {
                User = user,
                UpdateShopInfor = updateShopInfor
            };

            return Ok(await _mediator.Send(request));
        }

        [HttpPost("upload/avatar")]
        public async Task<IActionResult> UploadShopAvatar(UploadShopAvatarCommand request) {
            return Ok(await _mediator.Send(request));
        }
    }
}
