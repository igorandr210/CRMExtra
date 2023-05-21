using System.Threading.Tasks;
using Application.CronSettings.Commands;
using Application.CronSettings.DTOs;
using Application.CronSettings.Queries;
using Domain.Common;
using Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CronSettingController : BaseController
    {
        public CronSettingController(IMediator mediator) : base(mediator)
        {
        }
        
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<JobCronSettingDto>), 200)]
        [HttpPost("CreateCronSetting")]
        public async Task<IActionResult> CreateCronSetting(JobCronSettingDto cronSetting)
        {
            var result = await Mediator.Send(new CreateCronSettingCommand(cronSetting));

            return Ok(result.GetResponse());
        }
        
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<JobCronSettingDto>), 200)]
        [HttpGet("GetCronSetting")]
        public async Task<IActionResult> GetCronSetting([FromQuery] JobType jobType)
        {
            var result = await Mediator.Send(new GetCronSettingsQuery(jobType));

            return Ok(result.GetResponse());
        }
        
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<JobCronSettingDto>), 200)]
        [HttpPut("ChangeJob")]
        public async Task<IActionResult> ChangeJobStatus(ChangeJobDto jobStatusDto)
        {
            var result = await Mediator.Send(new ChangeJobStatusCommand(jobStatusDto));

            return Ok(result.GetResponse());
        }
    }
}