using Application.AssignmentTaskAttachments.Dtos;
using Application.Common.Extensions;
using Application.Common.Factories;
using Application.Documents.DTOs;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AssignmentTaskAttachments.Commands
{
    public record AddAttachmentsCommand(Guid AssignmentTaskId, List<IFormFile> Attachments) : IRequest<List<Guid>>;

    public class AddAttachmentsCommanddHandler : IRequestHandler<AddAttachmentsCommand, List<Guid>>
    {
        private readonly IStorageService _documentsStorageService;
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;

        public AddAttachmentsCommanddHandler(BucketServiceFactory bucketServiceFactory, IApplicationDbContext context, IUserService userService)
        {
            _documentsStorageService = bucketServiceFactory.Invoke(Common.Enums.BucketType.UsersAttachments);
            _context = context;
            _userService = userService;
        }

        public async Task<List<Guid>> Handle(AddAttachmentsCommand request, CancellationToken cancellationToken)
        {
            var assignmentTask = await _context.AssignmentTasks.FirstOrDefaultAsync(x => x.Id == request.AssignmentTaskId);
            var userId = _userService.UserId;
            var user = await _context.ProfileData.FirstOrDefaultAsync(x => x.Id == userId);
            var attachmentsList = new Collection<AssingmentTaskAttachments>();

            foreach (var file in request.Attachments)
            {
                var normalizedEmail = user.Email.NormalizeEmail();
                var key = $"{normalizedEmail}/AssignmentTask_{Guid.NewGuid()}";

                await _documentsStorageService.UploadFile(key, file);

                var entity = await _context.AssingmentTasksAttachments.AddAsync(new AssingmentTaskAttachments
                {
                    Key = key,
                    FileName = file.FileName,
                }, cancellationToken);

                attachmentsList.Add(entity.Entity);
            }

            assignmentTask.AssingmentTaskAttachmentsInfo = attachmentsList;

            await _context.SaveChangesAsync();

            return assignmentTask.AssingmentTaskAttachmentsInfo.Select(x => x.Id).ToList();
        }
    }
}