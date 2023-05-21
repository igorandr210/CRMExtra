using System;

namespace Application.IntakeForms.DTOs
{
    public class IntakeFormExcelDto
    {
        public string InsuredName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string IdForYear { get; set; }
        public string Zip { get; set; }
        public bool IsSpouse { get; set; }
        public string SpouseName { get; set; }
        public string SpouseEmail { get; set; }
        public string SpousePhone { get; set; }
        public string DateOfLoss { get; set; }
        public string InsuranceCompanyInfoValue { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? EffectivePolicyStartDate { get; set; }
        public DateTime? EffectivePolicyEndDate { get; set; }
        public bool? DateOfLossWithin { get; set; }
        public bool? PropertyOwner { get; set; }
        public bool? IsPropertyOccupied { get; set; }
        public string TypeOfLossInfoValue { get; set; }
        public bool? PrimaryResidence { get; set; }
        public string MailAddress { get; set; }
        public string StormInfoValue { get; set; }
        public string PropertyAccess { get; set; }
        public string InsuranceAgency { get; set; }
        public string NameOfAgent { get; set; }
        public string AgentPhone { get; set; }
        public string AgentEmail { get; set; }
        public bool Mortgage { get; set; }
        public string MortgageCompanyname { get; set; }
        public string MortgageLoanNumber { get; set; }
        public string MortgageReferredBy { get; set; }
        public string MortgageContractorName { get; set; }
        public string MortgageContractorEmail { get; set; }
        public bool ExteriorDamage { get; set; }
        public bool InteriorDamage { get; set; }
        public bool ContentsDamage { get; set; }
        public bool RoofDamage { get; set; }
        public DateTime? RoofReplaceDate { get; set; }
        public bool EmergencyService { get; set; }
        public bool ClaimFilled { get; set; }
        public string ClaimNumber { get; set; }
        public string ClaimInfo { get; set; }
        public DateTime? ClaimFilledDate { get; set; }
        public bool ClaimChecks { get; set; }
        public bool IsDraft { get; set; }
        public bool IsFilled { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
