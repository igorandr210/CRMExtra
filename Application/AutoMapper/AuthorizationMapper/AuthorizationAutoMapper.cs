using Application.Authorization.DTOs;
using Application.Common.DTOs;
using Application.ProfilesData.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using System.Linq;

namespace Application.AutoMapper.AuthorizationMapper
{
    public class AuthorizationAutoMapper : Profile
    {
        public AuthorizationAutoMapper()
        {
            CreateMap<ProfileData, SignUpDto>()
                .ForMember(x => x.ProfileId, opt => opt.MapFrom(x => x.IdForYear))
                .ReverseMap();
            CreateMap<ProfileData, UserByIdDto>()
               .ForMember(x => x.ProfileId, opt => opt.MapFrom(x => x.IdForYear))
               .ForMember(x => x.TypesOfLoss, opt => opt.MapFrom(x => x.IntakeForm.IntakeFormTypesOfLossInfo.Select(y => y.TypeOfLossInfo.Value).ToList()))
               .ForMember(x => x.DatesOfLoss, opt => opt.MapFrom(x => x.IntakeForm.DateOfLoss))
               .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.Id))
               .ReverseMap();
            CreateMap<UserInfoDto, ProfileData>()
                .ReverseMap();
            CreateMap<LoginResponseDto, ProfileData>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.UserId))
                .ReverseMap();
            CreateMap<PaginatedDataDto<ProfileData>, PaginatedDataDto<ProfileInfoDto>>()
                .ReverseMap();
            CreateMap<ProfileData, ProfileInfoDto>()
                .ForMember(x => x.ProfileId, opt => opt.MapFrom(x => x.IdForYear))
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.DatesOfLoss, opt => opt.MapFrom(x => x.IntakeForm.DateOfLoss))
                .ForMember(x => x.FullName, opt => opt.MapFrom(x => x.InsuredName))
                .ForMember(x => x.TypesOfLoss, opt => opt.MapFrom(x => x.IntakeForm.IntakeFormTypesOfLossInfo.Select(y => y.TypeOfLossInfo.Value).ToList()))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.IntakeForm.IsConfirmed ? IntakeFormStatus.Complete : IntakeFormStatus.Checked))
                .ReverseMap();
        }
    }
}
