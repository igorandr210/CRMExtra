using System;
using System.Collections.Generic;
using Application.Authorization.DTOs;
using Application.Documents.DTOs;
using Application.DropDownValues.DTOs;

namespace Application.IntakeForms.DTOs
{
    public class IntakeFormResponseDto
    {
        public Guid Id { get; set; }
        public DateTime[] DateOfLoss { get; set; }
        public string InsuranceCompany { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? EffectivePolicyStartDate { get; set; }
        public DateTime? EffectivePolicyEndDate { get; set; }
        public bool? DateOfLossWithin { get; set; }
        public bool? PropertyOwner { get; set; }
        public bool? IsPropertyOccupied { get; set; }
        public Guid? TypeOfOccupation { get; set; }
        public Guid? SelfOccupation { get; set; }
        public bool? PrimaryResidence { get; set; }
        public string MailAddress { get; set; }
        public Guid? StormName { get; set; }
        public string PropertyAccess { get; set; }
        public bool ExteriorDamage { get; set; }
        public bool InteriorDamage { get; set; }
        public bool ContentsDamage { get; set; }
        public Guid? RoofDamage { get; set; }
        public DateTime? RoofReplaceDate { get; set; }
        public bool EmergencyService { get; set; }
        public bool IsDraft { get; set; }
        public bool IsFilled { get; set; }
        public bool IsConfirmed { get; set; }
        
        public SpouseDto Spouse { get; set; }
        public MortgageDto Mortgage { get; set; }
        public ClaimDto Claim { get; set; }
        public InsuranceDto InsuranceAgency { get; set; }
        public List<FileInfoDto> Documents { get; set; }
        public SignUpDto UserData { get; set; }
        public List<ClaimCheckResponseDto> ClaimChecks { get; set; }
        public List<DropDownValue<string>> TypesOfLoss { get; set; }
    }
}