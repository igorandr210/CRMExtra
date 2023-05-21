using Application.Common.Factories;
using Application.Documents.Commands;
using Application.Documents.DTOs;
using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Documents.Queries
{
    public record GetFileLinkQuery(FileKeyDto Data) : IRequest<string>;
    public class GetFileLinkQueryHandler : IRequestHandler<GetFileLinkQuery, string>
    {
        private readonly IStorageService _documentsStorageService;

        public GetFileLinkQueryHandler(BucketServiceFactory bucketServiceFactory)
        {
            _documentsStorageService = bucketServiceFactory.Invoke(Common.Enums.BucketType.UsersAttachments);
        }

        public async Task<string> Handle(GetFileLinkQuery request, CancellationToken cancellationToken)
        {
            var fileLink = await _documentsStorageService.GetFileLink(request.Data.Key);

            return fileLink;
        }
    }
}
