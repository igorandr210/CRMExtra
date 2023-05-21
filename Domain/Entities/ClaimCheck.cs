using Domain.Common;
using System;

namespace Domain.Entities
{
    public class ClaimCheck : BaseAuditableEntity
    {
        public Guid IntakeFormId { get; set; }
        public Guid? DocumentId { get; set; }
        public long? ClaimAmount { get; set; }
        public DateTime? ClaimDate { get; set; }
        public DateTime? DateOfPostmark { get; set; }
        public virtual IntakeForm IntakeForm { get; set; }
        public virtual Document Document { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ClaimCheck castedClaim &&
                   Id.Equals(castedClaim.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
