using System;
using System.Threading.Tasks;
using Application.IntakeForms.Commands;
using Application.IntakeForms.DTOs;
using Application.IntakeForms.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class IntakeFormController : BaseController
    {
        public IntakeFormController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "Customer,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<IntakeFormResponseDto>), 200)]
        [HttpGet("Get/{userId:guid}")]
        public async Task<IActionResult> GetForm(Guid userId)
        {
            var result = await Mediator.Send(new GetIntakeFormByUserIdQuery(userId));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Customer,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<IntakeFormResponseDto>), 200)]
        [HttpPost("SaveDraft/{userId}")]
        public async Task<IActionResult> SaveDraft(IntakeFormRequestDto request, Guid userId)
        {
            var result = await Mediator.Send(new SaveDraftIntakeFormCommand(request, userId));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Customer", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<IntakeFormResponseDto>), 200)]
        [HttpPost("Submit/{userId:guid}")]
        public async Task<IActionResult> SaveForm(IntakeFormRequestDto request, Guid userId)
        {
            var result = await Mediator.Send(new SubmitIntakeFormCommand(request, userId));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<IntakeFormResponseDto>), 200)]
        [HttpPost("SubmitReview/{userId:guid}")]
        public async Task<IActionResult> SubmitReview(Guid userId)
        {
            var result = await Mediator.Send(new SubmitReviewIntakeFormCommand(userId));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Customer", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<bool>), 200)]
        [HttpGet("IsSubmited/{userId:guid}")]
        public async Task<IActionResult> IsSubmited(Guid userId)
        {
            var result = await Mediator.Send(new GetIsSubmitedQuery(userId));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Employee,Customer", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(FileStreamResult), 200)]
        [HttpGet("ExportExcel/{userId:guid}")]
        public async Task<IActionResult> ExportExcel(Guid userId)
        {
            var result = await Mediator.Send(new ExportExcelFileCommand(userId));

            return File(result.Stream, result.ContentType, result.Name);
        }

        [Authorize(Roles = "Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<bool>), 200)]
        [HttpPut("UnSubmitForm/{userId:guid}")]
        public async Task<IActionResult> UnSubmitForm(UnSubmitFormDto data, Guid userId)
        {
            var result = await Mediator.Send(new UnSubmitFormCommand(data, userId));

            return Ok(result.GetResponse());
        }
    }
}