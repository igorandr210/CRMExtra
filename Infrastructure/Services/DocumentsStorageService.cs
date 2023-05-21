using Amazon.S3;
using Application.Common.Enums;
using AutoMapper;

namespace Infrastructure.Services
{
    public class DocumentsStorageService : FilesStorageServiceBase
    {
        public override BucketType Type => BucketType.UsersAttachments;

        public DocumentsStorageService(AmazonS3Client client, IMapper mapper): base(client, mapper)
        {

        }
    }
}