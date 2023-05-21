using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.Common.Factories;
using Application.Documents.DTOs;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents.Commands
{
    public record SaveFileCommand(Guid UserId, FileUploadRequestDto FileUploadInfo) : IRequest<FileInfoDto>;

    public class SaveFileCommandHandler : IRequestHandler<SaveFileCommand, FileInfoDto>
    {
        private readonly IStorageService _documentsStorageService;
        private readonly IApplicationDbContext _context;

        public SaveFileCommandHandler(BucketServiceFactory bucketServiceFactory, IApplicationDbContext context)
        {
            _documentsStorageService = bucketServiceFactory.Invoke(Common.Enums.BucketType.UsersAttachments);
            _context = context;
        }
        
        public async Task<FileInfoDto> Handle(SaveFileCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.ProfileData.FirstOrDefaultAsync(x => x.Id == request.UserId);
            var normalizedEmail = user.Email.NormalizeEmail(); 
            var key = $"{normalizedEmail}/{request.FileUploadInfo.Type}_{Guid.NewGuid()}";

            await _documentsStorageService.UploadFile(key, request.FileUploadInfo.File);

            var userId = request.UserId;
            var intakeForm = await _context.IntakeForms
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            var entity = await _context.Documents.AddAsync(new Document
            {
                DocumentType = request.FileUploadInfo.Type,
                Key = key,
                IntakeFormId = intakeForm.Id,
                FileName = request.FileUploadInfo.File.FileName,
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            
            return new FileInfoDto
            {
                Id = entity.Entity.Id,
                FileName = entity.Entity.FileName,
                DocumentType = request.FileUploadInfo.Type,
                Key = key,
            };
        }
    }
}