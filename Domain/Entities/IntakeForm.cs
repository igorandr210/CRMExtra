using System;
using System.Collections.ObjectModel;
using Domain.Common;

namespace Domain.Entities
{
    public class IntakeForm:BaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public DateTime[] DateOfLoss { get; set; }
        public Guid? InsuranceCompany { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? EffectivePolicyStartDate { get; set; }
        public DateTime? EffectivePolicyEndDate { get; set; }
        public bool? DateOfLossWithin { get; set; }
        public bool? PropertyOwner { get; set; }
        public bool? IsPropertyOccupied { get; set; }
        public Guid? TypeOfOccupation { get; set; }
        public Guid? RoofDamage { get; set; }
        public Guid? SelfOccupation { get; set; }
        public bool? PrimaryResidence { get; set; }
        public string MailAddress { get; set; }
        public Guid? StormName { get; set; }
        public string PropertyAccess { get; set; }
        public bool ExteriorDamage { get; set; }
        public bool InteriorDamage { get; set; }
        public bool ContentsDamage { get; set; }
        public DateTime? RoofReplaceDate { get; set; }
        public bool EmergencyService { get; set; }
        public bool IsDraft { get; set; }
        public bool IsFilled { get; set; }
        public bool IsConfirmed { get; set; }

        public virtual Storm StormInfo { get; set; }
        public virtual Collection<IntakeFormTypeOfLoss> IntakeFormTypesOfLossInfo { get; set; }
        public virtual TypeOfOccupation TypeOfOccupationInfo { get; set; }
        public virtual InsuranceCompany InsuranceCompanyInfo { get; set; }
        public virtual ProfileData User { get; set; }
        public virtual Spouse Spouse { get; set; }
        public virtual Claim Claim { get; set; }
        public virtual InsuranceAgencyInfo InsuranceAgency { get; set; }
        public virtual MortgageInfo Mortgage { get; set; }
        public virtual RoofDamage RoofDamageInfo { get; set; }
        public virtual Collection<Document> Documents { get; set; }
        public virtual Collection<ClaimCheck> ClaimChecks { get; set; }
        public virtual Envelope Envelope { get; set; }
        public virtual SelfOccupation SelfOccupationInfo { get; set; }
    }
}
