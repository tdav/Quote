using QuoteServer.Core;
using QuoteServer.Database.Services;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quote.Database;

namespace QuoteServer.Extensions
{
    public static class MyDbContextService
    {
        public static void AddMyDbContext(this IServiceCollection services, IConfiguration conf)
        {    
            services
                .AddDbContext<MyDbContext>(opt => opt.UseInMemoryDatabase()); 
                .AddUnitOfWork<MyDbContext>();

            services.AddCustomRepository<tbUpdateApp, UpdaterAppService>();
            services.AddCustomRepository<tbUser, UserService>();
        }


        public static void UpdateMigrateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<MyDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
