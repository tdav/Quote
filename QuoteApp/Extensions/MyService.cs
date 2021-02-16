using Microsoft.Extensions.DependencyInjection;
using Quote.Repository;
using Quote.Senders;

namespace QuoteServer.Extensions
{
    public static class MyService
    {
        public static void AddMyService(this IServiceCollection services)
        {
            services.AddSingleton<ISender, EmailSender>();
            services.AddSingleton<ISender, SmsSender>();

            services.AddSingleton<ISerderService, SerderService>();
        }
    }
}
