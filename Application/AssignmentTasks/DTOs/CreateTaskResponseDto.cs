using System;
using Domain.Enum;

namespace Application.AssignmentTasks.DTOs
{
    public class CreateTaskResponseDto
    {
        public Guid Id { get; set; }
        public string TaskTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AssignedToId { get; set; }
        public Guid? AttachedProfileId { get; set; }
        public AssignmentTasksStatus Status { get; set; }
    }
}
