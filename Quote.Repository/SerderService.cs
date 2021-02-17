using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quote.Database;
using Quote.Senders;
using Quote.Senders.ViewModels;
using QuoteServer.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Quote.Repository
{
    public interface ISerderService
    {
        public Task<bool> SendAsync();
    }

    public class SerderService : ISerderService
    {
        private readonly IServiceScopeFactory service;

        public SerderService(IServiceScopeFactory _service)
        {
            service = _service;
        }

        public async Task<bool> SendAsync()
        {
            using (var scope = service.CreateScope())
            {

                var db = (MyDbContext)scope.ServiceProvider.GetRequiredService(typeof(MyDbContext));
                var sbList = await db.tbSubscribes
                                        .Include(i=>i.SubscribeUser)
                                        .Include(i=>i.Sender)
                                        .Where(x => x.SubscribeUser.Status == 1).ToListAsync();

                if (sbList.Count == 0) return true;


                var email = scope.ServiceProvider.GetServiceByName<ISender>("email");
                var sms = scope.ServiceProvider.GetServiceByName<ISender>("sms");


                var quote = await db.tbQuotes.AsNoTracking().OrderByDescending(o => o.CreateDate).FirstOrDefaultAsync(w => w.Status == 1);

                /*  
                                IUnitOfWork db = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                                var rp = db.GetRepository<tbSubscribe>();
                                var sbList = await rp.GetAllAsync(predicate: x => x.SubscribeUser.Status == 1);

                                if (sbList.Count == 0) return true;

                                var email = scope.ServiceProvider.GetRequiredService(typeof(EmailSender)) as EmailSender;  
                                var sms = scope.ServiceProvider.GetRequiredService(typeof(SmsSender)) as SmsSender;  

                                var qrp = db.GetRepository<tbQuote>();
                                var quList = qrp.GetAll(predicate: w => w.Status == 1, orderBy: x => x.OrderBy(o => o.CreateDate), disableTracking: true);
                                var quote = await quList.FirstOrDefaultAsync();
                                */

                foreach (var it in sbList)
                {
                    if (it.SenderId == 1)
                    {
                        var em = new viEmailModel();
                        em.Body = quote.Text;
                        em.Subject = "Quote of the Day";
                        em.ToEmail = it.SubscribeUser.Email;
                        await email.SendAsync(em);
                    }
                    else
                    {
                        var sm = new SmsModel();
                        sm.mes = quote.Text;
                        sm.mes_id = Guid.NewGuid().ToString();
                        sm.tel = it.SubscribeUser.Phone;
                        await sms.SendAsync(sm);
                    }

                    //var sender = scope.ServiceProvider.GetServiceByName<ISender>(it.Sender.Name);
                    //await sender.SendAsync(mes);
                }
            }
            return true;
        }
    }
}
