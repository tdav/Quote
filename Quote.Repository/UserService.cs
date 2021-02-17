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

        public UserService(MyDbContext _db, IConfiguration _conf) : base(_db)
        {
            db = _db;
            config = _conf;
        }

        public async Task<viUser> CreateUserAsync(viUserRegister value)
        {
            tbUser res = await db.tbUsers.AsNoTracking()
                                         .FirstOrDefaultAsync(x => x.Email == value.Email);

            if (res == null)
            {
                res = new tbUser
                {
                    LastName = value.LastName,
                    Name = value.Name,
                    Patronymic = value.Patronymic,
                    Email = value.Email,
                    Password = CHash.EncryptMD5( value.Password ),
                    Phone = value.Phone,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = 1,
                    Status = 1,
                    RoleId = 1
                };

                await db.tbUsers.AddAsync(res);
                await db.SaveChangesAsync();
            }
            else
            {
                if (res.Password != value.Password)
                    return new viUser() { Status = 0, StatusMessage = "User already exists" };
            }

            return GetToken(res);
        }


        public async Task<viUser> AuthenticateAsync(viAuthenticateModel model)
        {
            var res = await db.tbUsers
                              .AsNoTracking()
                              .Where(x => x.Email == model.Email)                             
                              .FirstOrDefaultAsync();

            if (res == null || CHash.EncryptMD5(model.Password) != res.Password) return null;

            return GetToken(res);
        } 

        private viUser GetToken(tbUser res)
        {
            var SecretStr = config.GetSection("JwtToken:SecretKey").Value;
            var key = Encoding.ASCII.GetBytes(SecretStr);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                           {
                               new Claim(ClaimTypes.Sid, res.Id.ToString()),
                               new Claim(ClaimTypes.Email, res.Email.ToString()),
                           }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var usr = new viUser();
            usr.Token = tokenHandler.WriteToken(token);
            usr.Email = res.Email;
            usr.Id = res.Id;

            return usr;
        }
    }
}
