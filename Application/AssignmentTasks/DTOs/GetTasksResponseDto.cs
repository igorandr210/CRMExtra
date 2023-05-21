using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Application.DropDownValues.DTOs;
using Application.IntakeForms.DTOs;
using Application.ProfilesData.Dto;

namespace Application.AssignmentTasks.DTOs
{
    public class GetTasksResponseDto
    {
        public Guid Id { get; set; }
        public GetTaskTypeDto TaskType { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public TaskProfileDto CreatedBy { get; set; }
        public TaskProfileDto AssignedTo { get; set; }
        public TaskAttachedProfileDto AttachedProfile { get; set; }
        public DateTime Created { get; set; }
        public AssignmentTasksStatus Status { get; set; }
        public List<DropDownValue<string>> TypesOfLoss { get; set; }
    }
}
