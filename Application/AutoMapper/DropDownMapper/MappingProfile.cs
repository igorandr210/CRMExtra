using Application.DropDownValues.DTOs;
using AutoMapper;
using Domain.Common;

namespace Application.AutoMapper.DropDownMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.State, DropDownValue<string>>()
                .ReverseMap();
            CreateMap<BaseDropDownEntity<string>, DropDownValue<string>>()
                .ReverseMap();
        }
    }
}