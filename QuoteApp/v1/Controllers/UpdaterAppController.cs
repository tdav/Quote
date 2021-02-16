using QuoteServer.Database.Services;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuoteServer.v1.Controllers
{
    [ApiController]   
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [SwaggerTag("Программани янгилаш")]
    public class UpdaterAppController : ControllerBase
    {
        private readonly IUnitOfWork db;
        private IConfiguration config;
        public UpdaterAppController(IUnitOfWork _db, IConfiguration _config)
        {
            db = _db;
            config = _config;
        }


        [HttpGet("/change-log")]
        public Task<List<viUpdateChangelog>> GetChangelog()
        {
            var rp = db.GetRepository<tbUpdateApp>(true) as UpdaterAppService;
            return rp.GetChangelog();
        }

        [HttpPost("/get-version")]
        public Task<viUpdateChangelog> GetVersion([FromBody] tbClientAppVersion appVersion)
        {
            var rp = db.GetRepository<tbUpdateApp>(true) as UpdaterAppService;
            return rp.GetLastVersion(appVersion);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> PostFile([FromForm] viUploadUpdateFile fileForm)
        {
            if (fileForm.File == null || fileForm.Name == "") return BadRequest("File not found");

            var rp = db.GetRepository<tbUpdateApp>(true) as UpdaterAppService;
            var res = await rp.AddNewUpdateAsync(fileForm.File, fileForm.Name, fileForm.Version, fileForm.ChangeLog);
            return Ok(res);
        }
         
    }
}
