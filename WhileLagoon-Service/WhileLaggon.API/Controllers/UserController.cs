using MediatR;
using Microsoft.AspNetCore.Mvc;
using WhileLaggon.API.Annotation;
using WhileLagoon.Application.Feature.UserFeature.Query.GetUserDetail;

namespace WhileLaggon.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class UserController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserDetail(Guid userId) {
            return Ok(await _mediator.Send(request: new GetUserDetailCommand(userId)));
        }
    }
}