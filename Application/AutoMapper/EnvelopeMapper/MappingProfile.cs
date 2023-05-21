using Application.DocuSign.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper.EnvelopeMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetEnvelopeDto, Envelope>()
                .ForMember(x => x.IsCustomerSign, opt => opt.MapFrom(x => x.IsCustomerSign))
                .ForMember(x => x.IsAdminSign, opt => opt.MapFrom(x => x.IsEmployeeSign))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.EnvelopeId)).ReverseMap();
        }
    }
}
