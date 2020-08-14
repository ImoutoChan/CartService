using System.Threading.Tasks;
using CartService.Infrastructure.Quartz;
using CartService.Services.Commands.Cart;
using MediatR;
using Microsoft.Extensions.Options;
using Quartz;

namespace CartService.Quartz
{
    public class GenerateReportsJob : IJob
    {
        private readonly IMediator _mediator;

        public GenerateReportsJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Execute(IJobExecutionContext context) => _mediator.Send(new GenerateReportCommand());

        public class Description : IQuartzJobDescription
        {
            private readonly string _cron;

            public Description(IOptions<QuartzSettings> options)
            {
                _cron = options.Value.GenerateReportDailyCron;
            }
            public IJobDetail GetJobDetails()
                => JobBuilder.Create<GenerateReportsJob>()
                    .WithIdentity("Generate cart report")
                    .Build();

            public ITrigger GetJobTrigger()
                => TriggerBuilder.Create()
                    .WithIdentity("Generate cart report")
                    .WithSchedule(CronScheduleBuilder.CronSchedule(_cron))
                    .Build();
        }
    }
}