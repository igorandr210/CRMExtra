using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Authorization.DTOs;
using Application.Common.DTOs;
using Application.ProfilesData.Commands;
using Application.ProfilesData.Dto;
using Application.ProfilesData.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProfilesController : BaseController
    {
        public ProfilesController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "Employee,Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<PaginatedDataDto<ProfileInfoDto>>), 200)]
        [HttpGet]
        public async Task<IActionResult> ProfileList([FromQuery]PaginationInfoProfileDto request)
        {
            var result = await Mediator.Send(new GetProfileListQuery(request));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(200)]
        [HttpDelete("DeleteProfile/{userId:guid}")]
        public async Task<IActionResult> DeleteProfile(Guid userId)
        {
            var result = await Mediator.Send(new DeleteProfileCommand(userId));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Employee,Customer", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(200)]
        [HttpGet("GetUserById/{userId:guid}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var result = await Mediator.Send(new GetUserByIdQuery(userId));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Employee,Customer", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(FileStreamResult), 200)]
        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel()
        {
            var result = await Mediator.Send(new ExportExcelFileQuery());
            
            return File(result.Stream, result.ContentType, result.Name);
        }
    }
}