using System;
using System.Threading.Tasks;
using Application.Documents.Commands;
using Application.Documents.DTOs;
using Application.Documents.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class DocumentsController : BaseController
    {
        public DocumentsController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "Customer,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<FileInfoDto>), 200)]
        [HttpPost("{userId:guid}")]
        public async Task<IActionResult> UploadDocument([FromForm] FileUploadRequestDto fileUploadInfo, Guid userId)
        {
            var result = await Mediator.Send(new SaveFileCommand(userId, fileUploadInfo));

            return Ok(result.GetResponse());
        }
        
        [Authorize(Roles = "Customer,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<FileInfoDto>), 200)]
        [HttpDelete]
        public async Task<IActionResult> DeleteDocument([FromQuery]Guid id)
        {
            var result = await Mediator.Send(new DeleteFileByIdCommand(id));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Customer,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseModelBase<string>), 200)]
        [HttpPost("CreatePreSignedLink")]
        public async Task<IActionResult> CreatePreSignedLink(FileKeyDto documentKey)
        {
            var result = await Mediator.Send(new GetFileLinkQuery(documentKey));

            return Ok(result.GetResponse());
        }

        [Authorize(Roles = "Customer,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(FileStreamResult), 200)]
        [HttpPost("DownloadFile")]
        public async Task<IActionResult> DownloadFile(FileKeyDto documentKey)
        {
            var result = await Mediator.Send(new DownloadFileQuery(documentKey));

            return File(result.Stream, result.ContentType, result.Name);
        }
    }   
}