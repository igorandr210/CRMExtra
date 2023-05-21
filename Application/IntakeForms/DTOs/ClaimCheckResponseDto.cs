using System;

namespace Application.IntakeForms.DTOs
{
    public class ClaimCheckResponseDto
    {
        public Guid? Id { get; set; }
        public long? ClaimAmount { get; set; }
        public DateTime? ClaimDate { get; set; }
        public DateTime? DateOfPostmark { get; set; }
        public Guid? DocumentId { get; set; }
        public string FileName { get; set; }
        public string DocumentKey { get; set; }
    }
}
