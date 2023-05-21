using System;
using System.Threading.Tasks;
using Application.Authorization.Commands.LoginUser;
using Application.Authorization.Commands.RegisterUser;
using Application.Authorization.DTOs;
using Application.Authorization.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace WebApi.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("SignIn")]
        [ProducesResponseType(typeof(ResponseModelBase<LoginResponseDto>), 200)]
        [ProducesResponseType(typeof(ResponseModelBase<LoginResponseDto>), 500)]
        public async Task<IActionResult> Login(LoginDto userInfo)
        {
            var result = await Mediator.Send(new LoginUserCommand(userInfo));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Admin,Employee,Customer", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetUserInfo")]
        [ProducesResponseType(typeof(ResponseModelBase<UserInfoDto>), 200)]
        public async Task<IActionResult> GetUserInfo()
        {
            var result = await Mediator.Send(new GetUserInfoQuery());

            return Ok(result.GetResponse());
        }
        
        [Authorize(Roles = "Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("CreateAccount")]
        [ProducesResponseType(typeof(ResponseModelBase<Guid?>), 200)]
        public async Task<IActionResult> CreateAccount(SignUpDto request)
        {
            var result = await Mediator.Send(new RegisterUserCommand(request));

            return Ok(result.GetResponse());
        }
    }
}
