using System;
using Domain.Common;

namespace Domain.Entities
{
    public class InsuranceAgencyInfo:BaseAuditableEntity
    {
        public Guid IntakeFormId { get; set; }
        public string InsuranceAgency { get; set; }
        public string NameOfAgent { get; set; }
        public string AgentPhone { get; set; }
        public string AgentEmail { get; set; }

        public virtual IntakeForm IntakeForm { get; set; }
        
        public override bool Equals(object obj)
        {
            return obj is InsuranceAgencyInfo castedInsuranceAgency &&
                   IntakeFormId.Equals(castedInsuranceAgency.IntakeFormId) &&
                   InsuranceAgency.Equals(castedInsuranceAgency.InsuranceAgency) &&
                   NameOfAgent.Equals(castedInsuranceAgency.NameOfAgent) &&
                   AgentPhone.Equals(castedInsuranceAgency.AgentPhone) &&
                   AgentEmail.Equals(castedInsuranceAgency.AgentEmail);
        }
    }
}
