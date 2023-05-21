using System;
using Domain.Enum;

namespace Application.Documents.DTOs
{
    public class FileInfoDto
    {
        public Guid Id { get; set; }
        public long? ClaimAmount { get; set; }
        public DateTime? ClaimDate { get; set; }
        public string FileName { get; set; }
        public string Key { get; set; }
        public DocumentType DocumentType { get; set; }
        public string FileLink { get; set; }
        public DateTime? DateOfPostmark { get; set; }
    }
}