using Application.Authorization.DTOs;
using System;
using System.Collections.Generic;

namespace Application.IntakeForms.DTOs
{
    public class IntakeFormRequestDto
    {
        public Guid Id { get; set; }
        public DateTime[] DateOfLoss { get; set; }
        public Guid? InsuranceCompany { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? EffectivePolicyStartDate { get; set; }
        public DateTime? EffectivePolicyEndDate { get; set; }
        public bool? DateOfLossWithin { get; set; }
        public bool? PropertyOwner { get; set; }
        public bool? IsPropertyOccupied { get; set; }
        public Guid? TypeOfOccupation { get; set; }
        public Guid?[] TypesOfLoss { get; set; }
        public Guid? SelfOccupation { get; set; }
        public bool? PrimaryResidence { get; set; }
        public string MailAddress { get; set; }
        public Guid? StormName { get; set; }
        public string PropertyAccess { get; set; }
        public bool ExteriorDamage { get; set; }
        public bool InteriorDamage { get; set; }
        public bool ContentsDamage { get; set; }
        public DateTime? RoofReplaceDate { get; set; }
        public Guid? RoofDamage { get; set; }
        public bool EmergencyService { get; set; }
        
        public MortgageDto Mortgage { get; set; }
        public SpouseDto Spouse { get; set; }
        public ClaimDto Claim { get; set; }
        public InsuranceDto InsuranceAgency { get; set; }
        public SignUpDto UserData { get; set; }
        public List<ClaimCheckRequestDto> ClaimChecks { get; set; }
    }
}