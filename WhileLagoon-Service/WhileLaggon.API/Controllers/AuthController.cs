using MediatR;
using Microsoft.AspNetCore.Mvc;
using WhileLagoon.Application.Feature.AuthFeature.Command.ActiveAccount;
using WhileLagoon.Application.Feature.AuthFeature.Command.ChangePassword;
using WhileLagoon.Application.Feature.AuthFeature.Command.ConfirmEmail;
using WhileLagoon.Application.Feature.AuthFeature.Command.ForgotPassword;
using WhileLagoon.Application.Feature.AuthFeature.Command.Login;
using WhileLagoon.Application.Feature.AuthFeature.Command.RefreshToken;
using WhileLagoon.Application.Feature.AuthFeature.Command.Register;
using WhileLagoon.Application.Feature.AuthFeature.Command.ResetPassword;
using WhileLagoon.Domain.Entity;

namespace WhileLaggon.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class AuthController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("/active/{token}")]
        public async Task<IActionResult> ActiveAccount(string token)
        {
            return Ok(await _mediator.Send(request: new ActiveAccountCommand(token)));
        }

        [HttpPost("/register")]
        public async Task<ActionResult> Register(RegisterCommand request)
        {
            User user = await _mediator.Send(request);
            return Ok(user);
        }

        [HttpPost("/login")]
        public async Task<ActionResult> Login(LoginCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPatch("/change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordCommand request) {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("/refresh-token")]
        public async Task<ActionResult> RefreshToken(RefreshTokenCommand request) {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("/forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordCommand request) {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("/confirm/email/{token}")]
        public async Task<ActionResult> ConfirmEmail(string token) {
            
            return Ok(await _mediator.Send(request: new ConfirmEmailCommand(token)));
        }

        [HttpPost("/reset-password/{token}")]
        public async Task<ActionResult> ResetPassword(string token, string password) {
            ResetPasswordCommand request = new()
            {
                Token = token,
                Password = password
            };
            return Ok(await _mediator.Send(request));
        }
    }
}
