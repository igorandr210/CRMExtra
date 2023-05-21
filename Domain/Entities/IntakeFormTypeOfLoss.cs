using System;
using Domain.Common;

namespace Domain.Entities
{
    public class IntakeFormTypeOfLoss : BaseAuditableEntity
    {
        public Guid IntakeFormId { get; set; }
        public Guid TypeOfLossId { get; set; }
        
        public virtual IntakeForm IntakeFormInfo { get; set; }
        public virtual TypeOfLoss TypeOfLossInfo { get; set; }

        public override bool Equals(object obj)
        {
            return obj is IntakeFormTypeOfLoss castedObj &&
                   IntakeFormId.Equals(castedObj.IntakeFormId)&&
                   TypeOfLossId.Equals(castedObj.TypeOfLossId);
        }
        
        public override int GetHashCode()
        {
            return (IntakeFormId.GetHashCode().ToString() + TypeOfLossId.GetHashCode().ToString()).GetHashCode();
        }
    }
}