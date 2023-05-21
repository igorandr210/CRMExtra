using Application.CronSettings.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper.CronSettingMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JobCronSettingDto, JobCronSetting>().ReverseMap();
        }
    }
}