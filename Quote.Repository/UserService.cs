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
    public class UserService : Repository<tbUser>, IRepository<tbUser>
    {
        private MyDbContext db;
        private IConfiguration config;

        public UserService(MyDbContext dbContext, IConfiguration _conf) : base(dbContext)
        {
            db = dbContext;
            config = _conf;
        }

        public async Task<viUser> AuthenticateAsync(viAuthenticateModel model)
        {
            viUser usr = new viUser();

            var res = await db.tbUsers
                              .AsNoTracking()
                              .Where(x => x.Login == model.Login)
                              .Select(x => new { x.Id, x.Login, x.Password })
                              .FirstOrDefaultAsync();

            if (res == null || CHash.EncryptMD5(model.Password) != res.Password) return null;

            var SecretStr = config.GetSection("JwtToken:SecretKey").Value;
            var key = Encoding.ASCII.GetBytes(SecretStr);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                           {
                               new Claim(ClaimTypes.Sid, res.Id.ToString()),
                               new Claim(ClaimTypes.Name, res.Login.ToString()),
                           }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            usr.Token = tokenHandler.WriteToken(token);
            usr.Username = res.Login;
            usr.Id = res.Id;

            return usr;
        }
    }
}
