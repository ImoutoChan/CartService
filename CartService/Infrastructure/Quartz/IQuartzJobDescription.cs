using Quartz;

namespace CartService.Infrastructure.Quartz
{
    public interface IQuartzJobDescription
    {
        IJobDetail GetJobDetails();

        ITrigger GetJobTrigger();
    }
}