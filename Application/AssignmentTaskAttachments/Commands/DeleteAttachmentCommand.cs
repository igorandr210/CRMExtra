using Application.Common.Factories;
using Application.Interfaces;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AssignmentTaskAttachments.Commands
{
    public record DeleteAttachmentCommand(Guid Id) : IRequest<Guid>;

    public class DeleteAttachmentCommanddHandler : IRequestHandler<DeleteAttachmentCommand, Guid>
    {
        private readonly IStorageService _documentsStorageService;
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;

        public DeleteAttachmentCommanddHandler(BucketServiceFactory bucketServiceFactory, IApplicationDbContext context, IUserService userService)
        {
            _documentsStorageService = bucketServiceFactory.Invoke(Common.Enums.BucketType.UsersAttachments);
            _context = context;
            _userService = userService;
        }

        public async Task<Guid> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
        {
            var attachment = await _context.AssingmentTasksAttachments.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (attachment is null)
            {
                throw new NotFoundException(request.Id.ToString());
            }

            await _documentsStorageService.DeleteFile(attachment.Key);

            _context.AssingmentTasksAttachments.Remove(attachment);
            await _context.SaveChangesAsync();

            return attachment.Id;
        }
    }
}
