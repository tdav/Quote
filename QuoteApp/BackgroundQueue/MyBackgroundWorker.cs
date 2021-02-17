using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using System;
using Quote.Repository;

namespace QuoteServer.BackgroundQueue
{
    public class MyBackgroundWorker : BackgroundService
    {
        private readonly IBackgroundQueue<string> queueMes;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ISerderService sender;

        public MyBackgroundWorker(IBackgroundQueue<string> _queueMes, IServiceScopeFactory _scopeFactory, ISerderService _serder)
        {
            scopeFactory = _scopeFactory;
            queueMes = _queueMes;
            sender = _serder;
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
                    foreach (var it in queueMes.GetAll())
                    {
                        await sender.SendAsync(it);
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
