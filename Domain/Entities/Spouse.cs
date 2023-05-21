using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Spouse:BaseAuditableEntity
    {
        public Guid IntakeFormId { get; set; }

        public bool IsSpouse { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual IntakeForm IntakeForm { get; set; }
        
        public override bool Equals(object obj)
        {
            return obj is Spouse castedSpouse &&
                   IntakeFormId.Equals(castedSpouse.IntakeFormId) &&
                   IsSpouse.Equals(castedSpouse.IsSpouse) &&
                   Name.Equals(castedSpouse.Name) &&
                   Email.Equals(castedSpouse.Email) &&
                   Phone.Equals(castedSpouse.Phone);
        }
    }
}