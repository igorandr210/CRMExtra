using System;
using System.Collections.ObjectModel;
using Domain.Common;
using Domain.Enum;

namespace Domain.Entities
{
    public class AssignmentTask : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AssignmentTasksStatus Status { get; set; }
        public Guid AssignedToId { get; set; }
        public Guid? AttachedProfileId { get; set; }
        public Guid TaskTypeId { get; set; }
        public virtual ProfileData AssignedUser { get; set; }
        public virtual ProfileData Creator { get; set; }
        public virtual ProfileData AttachedProfile { get; set; }
        public virtual TaskType TaskType { get; set; }
        public virtual Collection<AssingmentTaskAttachments> AssingmentTaskAttachmentsInfo { get; set; }
    }
}
