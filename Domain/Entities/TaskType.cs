using Domain.Common;
using System.Collections.ObjectModel;

namespace Domain.Entities
{
    public class TaskType : BaseDropDownEntity<string>
    {
        public Collection<AssignmentTask> AssignmentTasks { get; set; }
    }
}
