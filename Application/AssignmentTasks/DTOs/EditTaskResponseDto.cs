using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Application.AssignmentTasks.DTOs
{
    public class EditTaskResponseDto
    {
        public Guid Id { get; set; }
        public string StatusCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AssignmentTasksStatus Status { get; set; }
        public Guid AssignedToId { get; set; }
        public Guid? AttachedProfileId { get; set; }
        public Guid TaskTypeId { get; set; }
        public List<GetTaskAttachmentDto> AttachmentInfo { get; set; }

    }
}
