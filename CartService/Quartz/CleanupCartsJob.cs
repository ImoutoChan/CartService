using System.Threading.Tasks;
using CartService.Infrastructure.Quartz;
using CartService.Services.Commands.Cart;
using MediatR;
using Microsoft.Extensions.Options;
using Quartz;

namespace CartService.Quartz
{
    public class CleanupCartsJob : IJob
    {
        private readonly IMediator _mediator;

        public CleanupCartsJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Execute(IJobExecutionContext context) => _mediator.Send(new CleanupCartsCommand());

        public class Description : IQuartzJobDescription
        {
            private readonly int _repeatEveryMinutes;

            public Description(IOptions<CleanupCartsSettings> options)
            {
                _repeatEveryMinutes = options.Value.RepeatEveryMinutes;
            }
            public IJobDetail GetJobDetails()
                => JobBuilder.Create<CleanupCartsJob>()
                    .WithIdentity("Clean up old carts")
                    .Build();

            public ITrigger GetJobTrigger()
                => TriggerBuilder.Create()
                    .WithIdentity("Clean up old carts trigger")
                    .WithSchedule(SimpleScheduleBuilder.RepeatMinutelyForever(_repeatEveryMinutes))
                    .Build();
        }
    }
}