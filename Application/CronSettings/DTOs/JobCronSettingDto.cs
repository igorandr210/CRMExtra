using System;
using Domain.Enum;

namespace Application.CronSettings.DTOs
{
    public class JobCronSettingDto
    {
        public string CronSettingString { get; set; }
        public JobType JobType { get; set; }
        public bool Status { get; set; }
        public DateTime LastRunned { get; set; }
        public string ErrorNote { get; set; }
    }
}