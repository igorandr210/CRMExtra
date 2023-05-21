using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Envelope: BaseEntity
    {
        public Guid IntakeFormId { get;set; }
        public bool IsCustomerSign { get; set; }
        public bool IsAdminSign { get; set; }
        public string CustomersEnvelopeLink { get; set; }
        public IntakeForm IntakeForm { get; set; }
    }
}
