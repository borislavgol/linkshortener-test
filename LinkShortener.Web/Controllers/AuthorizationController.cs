using LinkShortener.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkShortener.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] SigninUserRequestDto signinUserRequestDto, CancellationToken cancellationToken)
        {
            var authorizationResult = await _mediator.Send(signinUserRequestDto, cancellationToken);

            if (!authorizationResult.IsSuccess)
            {
                return BadRequest("Invalid login or password");
            }

            var claims = new List<Claim>
            {
                new Claim("UserId", authorizationResult.UserId.ToString())
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

            return Ok();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignupUserRequestDto signupUserRequestDto, CancellationToken cancellationToken)
        {
            var creationResult = await _mediator.Send(signupUserRequestDto, cancellationToken);

            if (!creationResult.IsSuccess)
            {
                return BadRequest();
            }

            var claims = new List<Claim>
            {
                new Claim("UserId", creationResult.UserId.ToString())
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

            return Ok();
        }
    }
}
