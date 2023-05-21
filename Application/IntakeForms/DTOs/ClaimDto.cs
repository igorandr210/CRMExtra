using System;

namespace Application.IntakeForms.DTOs
{
    public class ClaimDto
    {
        public Guid Id { get; set; }
        public bool ClaimFilled { get; set; }
        public string ClaimNumber { get; set; }
        public string ClaimInfo { get; set; }
        public DateTime? ClaimFilledDate { get; set; }
        public bool IsClaimCheck { get; set; }
    }
}