using System;

namespace Domain.Common
{
    public class BaseAuditableEntity: BaseEntity
    {
        public DateTime Created { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public Guid? LastModifiedBy { get; set; }
    }
}
