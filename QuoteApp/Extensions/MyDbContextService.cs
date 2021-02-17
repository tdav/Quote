using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quote.Database;
using Quote.Database.Models;
using Quote.Repository;

namespace QuoteServer.Extensions
{
    public static class MyDbContextService
    {
        public static void AddMyDbContext(this IServiceCollection services, IConfiguration conf)
        {
            services
                 .AddDbContext<MyDbContext>(opt => opt.UseSqlite(conf.GetConnectionString("DefaultConnection"),
                                            ass => ass.MigrationsAssembly(typeof(MyDbContext).Assembly.FullName)))
                 .AddUnitOfWork<MyDbContext>();
        

            services.AddCustomRepository<tbUser, UserService>();
            services.AddCustomRepository<tbQuote, QuoteService>();
             
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
