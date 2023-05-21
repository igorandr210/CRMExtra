using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Claim:BaseAuditableEntity
    {
        public Guid IntakeFormId { get; set; }
        public bool ClaimFilled { get; set; }
        public string ClaimNumber { get; set; }
        public string ClaimInfo { get; set; }
        public DateTime? ClaimFilledDate { get; set; }
        public bool ClaimChecks { get; set; }
        

        public virtual IntakeForm IntakeForm { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Claim castedClaim &&
                   ClaimChecks.Equals(castedClaim.ClaimChecks) &&
                   IntakeFormId.Equals(castedClaim.IntakeFormId) &&
                   ClaimFilled.Equals(castedClaim.ClaimFilled) &&
                   ClaimNumber.Equals(castedClaim.ClaimNumber) &&
                   ClaimFilledDate.Equals(castedClaim.ClaimFilledDate);
        }
    }
}
