using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;  
using System;
using Quote.Database;

namespace QuoteServer.BackgroundQueue
{
    public class MyBackgroundWorker : BackgroundService
    {
        private readonly IBackgroundQueue<string> queueMes;
        private readonly IServiceScopeFactory scopeFactory;

        public MyBackgroundWorker(IBackgroundQueue<string> _queueMes, IServiceScopeFactory _scopeFactory)
        {
            scopeFactory = _scopeFactory;
            queueMes = _queueMes;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await BackgroundProcessing(stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = scopeFactory.CreateScope())
                    {
                        var ctx = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                        foreach (var it in queueMes.GetAll())
                    {
                        await emailSenderService.SendEmailAsync(it);
                    }

                    await Task.Delay(1000, stoppingToken);
                }
                catch (System.Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }
        }
    }
}
