using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Quote.Database.Models;
using Quote.Senders;
using Quote.Utils;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quote.Repository
{
    public interface ISerderService
    {
        public Task<bool> SendAsync(string mes);
    }

    public class SerderService : ISerderService
    {
        private readonly IUnitOfWork db;
        private readonly IHttpContextAccessor accessor;
        private readonly IServiceScopeFactory scopeFactory;

        public SerderService(IUnitOfWork _db, IHttpContextAccessor _accessor)
        {
            db = _db;
            accessor = _accessor;
        }

        public async Task<bool> SendAsync(string mes)
        {
            var user_id = accessor.HttpContext.User.FindFirst(ClaimTypes.Sid);
            
            var rp = db.GetRepository<tbSubscribe>();
            var sb = await rp.GetFirstOrDefaultAsync(predicate: x => x.SubscribeUserId == user_id.Value.ToInt());

            using (var scope = scopeFactory.CreateScope())
            {
                ISender sender;

                if (sb.SenderId == 1)
                    sender = scope.ServiceProvider.GetRequiredService<EmailSender>();
                else
                    sender = scope.ServiceProvider.GetRequiredService<SmsSender>();


                return true;
            }
        } 
    }
}
