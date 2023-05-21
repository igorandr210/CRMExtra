using System;
using System.Collections.Generic;
using Application.ProfilesData.Dto;
using Domain.Enum;

namespace Application.AssignmentTasks.DTOs
{
    public class GetTaskByIdResponseDto
    {
        public Guid Id { get; set; }
        public GetTaskTypeDto TaskType { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public TaskProfileDto CreatedBy { get; set; }
        public TaskProfileDto AssignedTo { get; set; }
        public TaskAttachedProfileDto AttachedProfile { get; set; }
        public List<GetTaskAttachmentDto> AttachmentInfo { get; set; }
        public DateTime Created { get; set; }
        public AssignmentTasksStatus Status { get; set; }
    }
}