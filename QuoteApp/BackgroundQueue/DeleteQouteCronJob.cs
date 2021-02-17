using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quote.Database.Models;
using Quote.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuoteServer.BackgroundQueue
{
    public class DeleteQouteCronJob : CronJobService
    {
        private readonly ILogger<DeleteQouteCronJob> logger;
        private readonly IServiceProvider serviceProvider;

        public DeleteQouteCronJob(IScheduleConfig<DeleteQouteCronJob> config, ILogger<DeleteQouteCronJob> _logger, IServiceProvider _serviceProvider)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            logger = _logger;
            serviceProvider = _serviceProvider;
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            logger.LogInformation($"{DateTime.Now:hh:mm:ss} CronJob 1 is working.");

            try
            {
                using var scope = serviceProvider.CreateScope();
                {
                    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var rp = uow.GetRepository<tbQuote>(true) as QuoteService;
                    await rp.QuoteDelete();
                }
            }
            catch (System.Exception ee)
            {
                logger.LogError(ee, "MessageSenderCronJob.DoWork");
            }
        }        
    }
}
