using System;
using System.Collections.ObjectModel;
using Domain.Common;
using Domain.Enum;

namespace Domain.Entities
{
    public class Document:BaseAuditableEntity
    {
        public Guid IntakeFormId { get; set; }
        public string Key { get; set; }
        public string FileName { get; set; }
        public DocumentType DocumentType { get; set; }
        public virtual IntakeForm IntakeForm { get; set; }
        public virtual ClaimCheck ClaimCheck { get; set; }
    }
}