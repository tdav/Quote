using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Quote.Database;
using Quote.Database.Models;
using System;
using System.Threading.Tasks;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quote.Database;
using Quote.Database.Models;
using Quote.Repository.ViewModels;
using Quote.Utils;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Repository
{
    public class QuoteService : Repository<tbQuote>, IRepository<tbQuote>
    {
        private MyDbContext db;
        private IConfiguration config;

        public QuoteService(MyDbContext _db, IConfiguration _conf) : base(_db)
        {
            db = _db;
            config = _conf;
        }



        public async Task QuoteDelete()
        {
            var list = await db.tbQuotes.AsNoTracking()
                                        .Where(x => x.CreateDate > DateTime.Now.AddDays(-1))
                                        .ToListAsync();
            if (list.Count > 0)
            {
                db.tbQuotes.RemoveRange(list);
            }
        }

        public async Task<tbQuote> RandomQuoteAsync()
        {
            var cnt = db.tbQuotes.Count(predicate: x => x.Status == 1);
            var rnd = new Random();
            var index = rnd.Next(1, cnt);
            var item = await db.tbQuotes.Skip(index).Take(1).FirstOrDefaultAsync();

            return item;
        }
    }
}
