using Domain.Common;

namespace Domain.Entities
{
    public class RoofDamage : BaseDropDownEntity<string>
    {
        public virtual IntakeForm IntakeForm { get; set; }
    }
}
