using Applicatio.DocuSign.Commands;
using Application.DocuSign.Commands;
using Application.DocuSign.DTOs;
using Application.DocuSign.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class DocuSignController : BaseController
    {
        public DocuSignController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<GetEnvelopeDto>), 200)]
        [HttpPost("SendDocuSign/{userId:guid}")]
        public async Task<IActionResult> SendDocuSign(Guid userId)
        {
            var result = await Mediator.Send(new SendDocuSignCommand(userId));

            return Ok(result.GetResponse());
        }
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ResponseModelBase<GetEnvelopeDto>), 200)]
        [HttpPost("UpdateDocuSign")]
        public async Task<IActionResult> UpdateEnvelope(EnvelopeDto envelopeInfo)
        {
            var result = await Mediator.Send(new UpdateEnvelopeCommand(envelopeInfo));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Customer,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<GetEnvelopeDto>), 200)]
        [HttpGet("GetEnvelope/{userId:guid}")]
        public async Task<IActionResult> GetEnvelope(Guid userId)
        {
            var result = await Mediator.Send(new GetEnvelopeQuery(userId));

            return Ok(result.GetResponse());
        }
    }
}
