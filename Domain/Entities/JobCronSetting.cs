using System;
using Domain.Common;
using Domain.Enum;

namespace Domain.Entities
{
    public class JobCronSetting : BaseEntity
    {
        public string CronSettingString { get; set; }
        public JobType JobType { get; set; }
        public bool Status { get; set; }
        public DateTime LastRunned { get; set; }
        public string ErrorNote { get; set; }
    }
}