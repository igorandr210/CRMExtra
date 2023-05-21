using System.Linq;
using Application.AssignmentTasks.DTOs;
using Application.Common.DTOs;
using AutoMapper;
using Domain.Entities;
using static Newtonsoft.Json.Serialization.JsonLinqContract;

namespace Application.AutoMapper.AssignmentTaskMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AssignmentTask, GetTasksResponseDto>()
                .ForMember(x => x.Label, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.CreatedBy, opt => opt.MapFrom(x => x.Creator))
                .ForMember(x => x.AssignedTo, opt => opt.MapFrom(x => x.AssignedUser))
                .ForMember(x => x.AttachedProfile, opt => opt.MapFrom(x => x.AttachedProfile))
                .ForMember(x => x.TypesOfLoss, opt => opt.MapFrom(x => x.AttachedProfile.IntakeForm.IntakeFormTypesOfLossInfo.Select(y => y.TypeOfLossInfo).ToList()))
                .ReverseMap();
            CreateMap<AssignmentTask, GetTaskByIdResponseDto>()
                .ForMember(x => x.Label, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.CreatedBy, opt => opt.MapFrom(x => x.Creator))
                .ForMember(x => x.AssignedTo, opt => opt.MapFrom(x => x.AssignedUser))
                .ForMember(x => x.AttachedProfile, opt => opt.MapFrom(x => x.AttachedProfile))
                .ForMember(x=>x.AttachmentInfo, opt=>opt.MapFrom(x=>x.AssingmentTaskAttachmentsInfo));
            CreateMap<PaginatedDataDto<AssignmentTask>, PaginatedDataDto<GetTasksResponseDto>>().ReverseMap();
            CreateMap<CreateTaskRequestDto, AssignmentTask>();
            CreateMap<AssignmentTask, CreateTaskResponseDto>().ReverseMap();
            CreateMap<EditTaskRequestDto, AssignmentTask>();
            CreateMap<AssignmentTask, EditTaskResponseDto>()
                .ForMember(x => x.AttachmentInfo, opt => opt.MapFrom(x => x.AssingmentTaskAttachmentsInfo));
            CreateMap<TaskType, GetTaskTypeDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Value))
                .ReverseMap();
            CreateMap<GetTaskAttachmentDto, AssingmentTaskAttachments>()
                .ForMember(x => x.FileName, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Key, opt => opt.MapFrom(x => x.Key))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();
        }
    }
}