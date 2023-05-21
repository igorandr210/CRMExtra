using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ProfilesData.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;

namespace Application.AutoMapper.ProfilesMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProfileData, FileExportProfileDto>()
                .ForMember(x => x.ProfileId, opt => opt.MapFrom(x => x.IdForYear))
                .ForMember(x => x.DateOfLoss,
                    opt => opt.MapFrom(x => string.Join("\n",
                        x.IntakeForm.DateOfLoss.Select(d => d.ToString("MM/dd/yyyy", new CultureInfo("en-US"))))))
                .ForMember(x => x.InsuredName, opt => opt.MapFrom(x => x.InsuredName))
                .ForMember(x => x.TypeOfLoss,
                    opt => opt.MapFrom(x => string.Join("\n",
                        x.IntakeForm.IntakeFormTypesOfLossInfo.Select(d => d.TypeOfLossInfo.Value))))
                .ForMember(x => x.Status,
                    opt => opt.MapFrom(x =>
                        x.IntakeForm.IsConfirmed ? IntakeFormStatus.Complete : IntakeFormStatus.Checked))
                .ForMember(x => x.HasIntakeForm, opt => opt.MapFrom((x => x.IntakeForm != null)))
                .ReverseMap();
            CreateMap<ProfileData, TaskProfileDto>()
                .ForMember(x => x.ProfileId, opt => opt.MapFrom(x => x.IdForYear));
            CreateMap<ProfileData, TaskAttachedProfileDto>()
                .ForMember(x => x.ProfileId, opt => opt.MapFrom(x => x.IdForYear))
                .ForMember(x => x.IntakeFormStatus, opt => opt.MapFrom(x =>
                    x.IntakeForm.IsConfirmed ? IntakeFormStatus.Complete : IntakeFormStatus.Checked));
        }
    }
}