using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories;
using Application.Interfaces;
using Domain.Enum;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents.Commands
{
    public record DeleteFileByIdCommand(Guid Id) : IRequest<bool>;

    public class DeleteFileByIdCommandHandler : IRequestHandler<DeleteFileByIdCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IStorageService _documentsStorageService;
        private readonly IUserService _userService;

        public DeleteFileByIdCommandHandler(IApplicationDbContext context, BucketServiceFactory bucketServiceFactory, IUserService userService)
        {
            _context = context;
            _documentsStorageService = bucketServiceFactory.Invoke(Common.Enums.BucketType.UsersAttachments);
            _userService = userService;
        }        
        
        public async Task<bool> Handle(DeleteFileByIdCommand request, CancellationToken cancellationToken)
        {
            Guid? userId = _userService.HasRole(Role.Employee) ? null : _userService.UserId;
            
            var existedDocument = await _context.Documents
                .Include(x=> x.IntakeForm)
                .Include(x=>x.ClaimCheck)
                .FirstOrDefaultAsync(x => x.Id == request.Id && (x.IntakeForm.UserId == userId || userId == null), 
                    cancellationToken);
            
            if (existedDocument is null)
            {
                throw new NotFoundException(request.Id.ToString());
            }
            await _documentsStorageService.DeleteFile(existedDocument.Key);

            _context.Documents.Remove(existedDocument);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}