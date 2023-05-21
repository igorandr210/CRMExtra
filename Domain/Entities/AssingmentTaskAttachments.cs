using Domain.Common;
using System;

namespace Domain.Entities
{
    public class AssingmentTaskAttachments : BaseAuditableEntity
    {
        public Guid AssingmentTaskId { get; set; }
        public string Key { get; set; }
        public string FileName { get; set; }

        public virtual AssignmentTask AssignmentTaskInfo { get; set; }
    }
}
