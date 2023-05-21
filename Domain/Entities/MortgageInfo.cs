using System;
using Domain.Common;

namespace Domain.Entities
{
    public class MortgageInfo:BaseAuditableEntity
    {
        public Guid IntakeFormId { get; set; }
        public bool Mortgage { get; set; }
        public string Companyname { get; set; }
        public string LoanNumber { get; set; }
        public string ReferredBy { get; set; }
        public string ContractorName { get; set; }
        public string ContractorEmail { get; set; }

        public virtual IntakeForm IntakeForm { get; set; }
        
        public override bool Equals(object obj)
        {
            return obj is MortgageInfo castedMortgageInfo &&
                   IntakeFormId.Equals(castedMortgageInfo.IntakeFormId) &&
                   Mortgage.Equals(castedMortgageInfo.Mortgage) &&
                   Companyname.Equals(castedMortgageInfo.Companyname) &&
                   LoanNumber.Equals(castedMortgageInfo.LoanNumber) &&
                   ContractorName.Equals(castedMortgageInfo.ContractorName) &&
                   ContractorEmail.Equals(castedMortgageInfo.ContractorEmail) &&
                   ReferredBy.Equals(castedMortgageInfo.ReferredBy);
        }
    }
}
