using System;
using Domain.Enum;

namespace Application.ProfilesData.Dto
{
    public class FileExportProfileDto
    {
        public string ProfileId { get; set; }
        public string InsuredName { get; set; }
        public string DateOfLoss { get; set; }
        public string TypeOfLoss { get; set; }
        public string Status { get; set; }
        public bool HasIntakeForm { get; set; }
    }
}
