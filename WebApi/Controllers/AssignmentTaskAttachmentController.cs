using Application.AssignmentTaskAttachments.Commands;
using Application.AssignmentTaskAttachments.Dtos;
using Application.AssignmentTasks.DTOs;
using Application.AssignmentTasks.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class AssignmentTaskAttachmentController : BaseController
    {
        public AssignmentTaskAttachmentController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "Employee,Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Post")]
        [ProducesResponseType(typeof(ResponseModelBase<List<Guid>>), 200)]
        public async Task<IActionResult> Post([FromForm] List<IFormFile> attachments, Guid assignmentTaskId)
        {
            var result = await Mediator.Send(new AddAttachmentsCommand(assignmentTaskId, attachments));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Employee,Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Delete/{id:guid}")]
        [ProducesResponseType(typeof(ResponseModelBase<List<GetTasksResponseDto>>), 200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteAttachmentCommand(id));

            return Ok(result.GetResponse());
        }
    }
}
