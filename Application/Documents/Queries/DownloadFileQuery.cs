using Application.Common.Factories;
using Application.Documents.DTOs;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Documents.Queries
{
    public record DownloadFileQuery(FileKeyDto Data) : IRequest<DownloadFileDto>;

    public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, DownloadFileDto>
    {
        private readonly IStorageService _documentsStorageService;

        public DownloadFileQueryHandler(BucketServiceFactory bucketServiceFactory)
        {
            _documentsStorageService = bucketServiceFactory.Invoke(Common.Enums.BucketType.UsersAttachments);
        }

        public Task<DownloadFileDto> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            return _documentsStorageService.DownloadFile(request.Data.Key);
        }
    }
}
