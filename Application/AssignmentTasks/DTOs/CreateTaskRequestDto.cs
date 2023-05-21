using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Application.AssignmentTasks.DTOs
{
    public class CreateTaskRequestDto
    {
        public Guid TaskTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AssignedToId { get; set; }
        public Guid? AttachedProfileId { get; set; }
    }
}
