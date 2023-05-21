using System;
using Application.Common.DTOs;
using Application.Common.Enums;
using Domain.Enum;

namespace Application.AssignmentTasks.DTOs
{
    public class PaginationInfoTaskDto : PaginationInfoDto
    {
        public string AttachedProfileSearchString { get; set; }
        public string AssignedProfileSearchString { get; set; }
        public string CreatorProfileSearchString { get; set; }
        public AssignmentTasksStatus[] Statuses { get; set; } = new AssignmentTasksStatus[] { AssignmentTasksStatus.Created, AssignmentTasksStatus.Done, AssignmentTasksStatus.InProgress };
        public Guid?[] TypesOfLoss { get; set; }
        public DateTime? DateOfLoss { get; set; }
        public TasksOwnership? TasksOwnership { get; set; }
    }
}
