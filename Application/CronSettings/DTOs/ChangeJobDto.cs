using Domain.Enum;

namespace Application.CronSettings.DTOs
{
    public class ChangeJobDto
    {
        public JobType JobType { get; set; }
        public bool Status { get; set; }
        public string CronString { get; set; }
    }
}