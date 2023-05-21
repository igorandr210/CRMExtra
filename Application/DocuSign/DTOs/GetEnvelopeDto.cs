using System;

namespace Application.DocuSign.DTOs
{
    public class GetEnvelopeDto
    {
        public Guid EnvelopeId { get; set; }
        public bool IsCustomerSign { get; set; }
        public bool IsEmployeeSign { get; set; }
    }
}
