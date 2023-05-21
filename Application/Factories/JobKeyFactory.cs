using Domain.Enum;
using Quartz;

namespace Application.Factories
{
    public delegate JobKey JobKeyFactory(JobType jobType);
}