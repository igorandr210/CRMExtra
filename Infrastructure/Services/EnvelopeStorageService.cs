using Amazon.S3;
using Application.Common.Enums;
using AutoMapper;

namespace Infrastructure.Services
{
    public class EnvelopeStorageService : FilesStorageServiceBase
    {
        public override BucketType Type => BucketType.EnvelopesCRM;

        public EnvelopeStorageService(AmazonS3Client client, IMapper mapper) : base(client, mapper)
        {

        }
    }
}