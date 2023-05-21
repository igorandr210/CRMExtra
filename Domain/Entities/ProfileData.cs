using System;
using System.Collections;
using System.Collections.Generic;
using Domain.Common;
using Domain.Enum;

namespace Domain.Entities
{
    public class ProfileData:BaseAuditableEntity
    {
        public string Email { get; set; }
        public string InsuredName { get; set; }
        public string AddressLine1 { get; set; }
        public string Phone { get; set; }
        public Guid? State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public bool IsDeleted { get; set; }
        public Role[] Roles { get; set; }
        public string IdForYear { get; set; }

        public virtual State StateInfo { get; set; }
        public virtual IntakeForm IntakeForm { get; set; }
        public virtual ICollection<AssignmentTask> CreatedTasks { get; set; }
        public virtual ICollection<AssignmentTask> AssignedTasks { get; set; }
        public virtual ICollection<AssignmentTask> AttachedToTasks { get; set; }
    }
}
