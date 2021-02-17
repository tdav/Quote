using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quote.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuoteServer.BackgroundQueue
{
    public class MessageSenderCronJob : CronJobService
    {
        private readonly ILogger<MessageSenderCronJob> logger;
        private readonly IServiceProvider service; 

        public MessageSenderCronJob(IScheduleConfig<MessageSenderCronJob> config, ILogger<MessageSenderCronJob> _logger, IServiceProvider _service)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            logger = _logger;
            service = _service;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Message Sender CronJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            logger.LogInformation($"{DateTime.Now:hh:mm:ss} CronJob 1 is working.");

            try
            {
                using var scope = service.CreateScope();
                {
                    var svc = scope.ServiceProvider.GetRequiredService<ISerderService>();
                    await svc.SendAsync();
                }
            }
            catch (System.Exception ee)
            {
                logger.LogError(ee, "MessageSenderCronJob.DoWork");
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Message Sender CronJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
