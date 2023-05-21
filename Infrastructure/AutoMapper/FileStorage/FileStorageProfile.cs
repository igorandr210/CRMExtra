using Amazon.S3.Model;
using Application.Documents.DTOs;
using AutoMapper;

namespace Infrastructure.AutoMapper.FileStorage
{
    public class FileStorageProfile : Profile
    {
        public FileStorageProfile()
        {
            CreateMap<GetObjectResponse, DownloadFileDto>()
                .ForMember(x => x.ContentType, opt => opt.MapFrom(x => x.Headers.ContentType))
                .ForMember(x => x.Stream, opt => opt.MapFrom(x => x.ResponseStream));
        }
    }
}
