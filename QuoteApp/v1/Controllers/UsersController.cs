using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Quote.Database.Models;
using Quote.Repository;
using Quote.Repository.ViewModels;

namespace QuoteServer.v1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [SwaggerTag("Users")]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork db;
        private ILogger<UsersController> logger;

        public UsersController(IUnitOfWork _db, ILogger<UsersController> _logger)
        {
            db = _db;
            logger = _logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerOperation("Login")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] viAuthenticateModel model)
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            if (string.IsNullOrEmpty( model.Email ) && string.IsNullOrEmpty(model.Password))
            {
                logger.LogInformation($"Login Empty User:{model.Email} Passw:{model.Password} Ip:{remoteIpAddress}");
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var rpUser = db.GetRepository<tbUser>(true) as UserService;
            var user = await rpUser.AuthenticateAsync(model);

            if (user == null)
            {
                logger.LogInformation($"Login BadRequest User:{model.Email} Passw:{model.Password} Ip:{remoteIpAddress}");
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            else
            {
                logger.LogInformation($"Login Ok User:{model.Email} Ip:{remoteIpAddress}");
            }

            return Ok(user);
        }


        [HttpPost("register")]
        [SwaggerOperation("Register")]
        public async Task<IActionResult> UserRegisterAsync([FromBody] viUserRegister model)
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            if (string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(model.Password))
            {
                logger.LogInformation($"Login Empty User:{model.Email} Passw:{model.Password} Ip:{remoteIpAddress}");
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var rpUser = db.GetRepository<tbUser>(true) as UserService;
            var user = await rpUser.CreateUserAsync(model);

            if (user == null)
            {
                logger.LogInformation($"Login BadRequest User:{model.Email} Passw:{model.Password} Ip:{remoteIpAddress}");
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            else
            {
                logger.LogInformation($"Login Ok User:{model.Email} Ip:{remoteIpAddress}");
            }

            return Ok(user);
        }
    }
}
