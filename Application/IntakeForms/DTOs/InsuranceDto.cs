using System;

namespace Application.IntakeForms.DTOs
{
    public class InsuranceDto
    {
        public Guid Id { get; set; }
        public string InsuranceAgency { get; set; }
        public string NameOfAgent { get; set; }
        public string AgentPhone { get; set; }
        public string AgentEmail { get; set; }
    }
}