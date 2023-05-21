using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.DropDownValues.Commands;
using Application.DropDownValues.DTOs;
using Application.DropDownValues.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class DropDownController : BaseController
    {
        public DropDownController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModelBase<List<DropDownValue<string>>>), 200)]
        public async Task<IActionResult> Get([FromQuery] DropDownType type)
        {
            var result = await Mediator.Send(new GetDropDownValuesQuery(type));

            return Ok(result.GetResponse());
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<DropDownValue<string>>), 200)]
        public async Task<IActionResult> Create([FromBody] CreateDropdownRequestDto request)
        {
            var result = await Mediator.Send(new CreateDropDownValueCommand(request));

            return Ok(result.GetResponse());
        }
    }
}