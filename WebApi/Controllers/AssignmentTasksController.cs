using System;
using Application.AssignmentTasks.DTOs;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.AssignmentTasks.Commands;
using Application.AssignmentTasks.Queries;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
    public class AssignmentTasksController : BaseController
    {
        public AssignmentTasksController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "Employee,Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Get")]
        [ProducesResponseType(typeof(ResponseModelBase<List<GetTasksResponseDto>>), 200)]
        public async Task<IActionResult> Get([FromBody]PaginationInfoTaskDto pagination)
        {
            var result = await Mediator.Send(new GetTasksQuery(pagination));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Employee,Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{taskId:guid}")]
        [ProducesResponseType(typeof(ResponseModelBase<GetTaskByIdResponseDto>), 200)]
        public async Task<IActionResult> GetById(Guid taskId)
        {
            var result = await Mediator.Send(new GetTaskByIdQuery(taskId));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Employee,Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseModelBase<CreateTaskResponseDto>), 200)]
        public async Task<IActionResult> Post(CreateTaskRequestDto data)
        {
            var result = await Mediator.Send(new CreateTaskCommand(data));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Employee,Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{taskId:guid}")]
        [ProducesResponseType(typeof(ResponseModelBase<GetTaskByIdResponseDto>), 200)]
        public async Task<IActionResult> Put(Guid taskId, [FromBody]EditTaskRequestDto data)
        {
            var result = await Mediator.Send(new EditTaskCommand(data, taskId));

            return Ok(result.GetResponse());
        }
    }
}
