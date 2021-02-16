using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quote.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Quote.Repository
{

    public class UpdaterAppService : Repository<tbUpdateApp>, IRepository<tbUpdateApp>
    {
        private readonly MyDbContext db;
        private readonly IConfiguration conf;
        private readonly IHttpContextAccessor accessor;

        public UpdaterAppService(MyDbContext dbContext, IConfiguration _conf, IHttpContextAccessor _accessor) : base(dbContext)
        {
            db = dbContext;
            conf = _conf;
            accessor = _accessor;
        }

        public Task<List<viUpdateChangelog>> GetChangelog()
        {
            string path = $"/{conf.GetValue<string>("SystemParams:UploadFilesPath")}/";
            return db.tbUpdateApps.AsNoTracking()
                                   .Select(s => new viUpdateChangelog
                                   {
                                       Version = s.Version,
                                       ChangeLog = s.ChangeLog,
                                       CreateDate = s.CreateDate,
                                       Path=path+s.Path
                                   })
                                   .ToListAsync();
        }

        public async Task<viUpdateChangelog> GetLastVersion(tbClientAppVersion val)
        {
            var client = await db.tbClientAppVersions.FirstOrDefaultAsync(x => x.MacAddress == val.MacAddress);
            if (client == null)
            {
                db.tbClientAppVersions.Add(val);
            }
            else
            {
                if (client.Version != val.Version)
                    client.Version = val.Version;
            }

            await db.SaveChangesAsync();
            string path = $"/{conf.GetValue<string>("SystemParams:UploadFilesPath")}/";
            return await db.tbUpdateApps.AsNoTracking()
                                        .OrderBy(o => o.Version)
                                        .Where(x => x.Version > val.Version)
                                        .Select(s => new viUpdateChangelog
                                        {
                                            Version = s.Version,
                                            ChangeLog = s.ChangeLog,
                                            CreateDate = s.CreateDate,
                                            Path = path+s.Path
                                        })
                                        .FirstOrDefaultAsync();
        }

        public async Task<bool> AddNewUpdateAsync(IFormFile file, string name, int Version, string ChangeLog)
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}wwwroot/{conf.GetValue<string>("SystemParams:UploadFilesPath")}/";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string fileName = $"{DateTime.UtcNow.ToString("yyyyMMdd_HHmmss")}_{name}";
            using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var ua = new tbUpdateApp();
            ua.Path = fileName;
            ua.Version = Version;
            ua.ChangeLog = ChangeLog;
            ua.CreateDate = DateTime.Now;
            ua.CreateUser = accessor.HttpContext.User.GetId();
            await db.tbUpdateApps.AddAsync(ua);
            await db.SaveChangesAsync();

            return true;
        }
    }
}
