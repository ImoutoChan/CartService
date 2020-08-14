using Quartz;

namespace CartService.Infrastructure.Quartz
{
    public static class SchedulerFactoryExtensions
    {
        public static IScheduler GetSchedulerSynchronously(this ISchedulerFactory factory)
            => factory.GetScheduler().Result;
    }
}