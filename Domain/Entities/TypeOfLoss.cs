using System.Collections.ObjectModel;
using Domain.Common;

namespace Domain.Entities
{
    public class TypeOfLoss: BaseDropDownEntity<string>
    {
        public Collection<IntakeFormTypeOfLoss> IntakeFormTypeofLossInfo { get; set; }
    }
}