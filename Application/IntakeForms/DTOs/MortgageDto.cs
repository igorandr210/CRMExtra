using System;

namespace Application.IntakeForms.DTOs
{
    public class MortgageDto
    {
        public Guid Id { get; set; }
        public bool Mortgage { get; set; }
        public string Companyname { get; set; }
        public string LoanNumber { get; set; }
        public string ReferredBy { get; set; }
        public string ContractorName { get; set; }
        public string ContractorEmail { get; set; }
    }
}