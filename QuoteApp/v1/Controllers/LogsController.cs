using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace QuoteServer.v1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [SwaggerTag("Logs")]
    public class LogsController : ControllerBase
    {
        private readonly IUnitOfWork db;

        public LogsController(IUnitOfWork _db) => db = _db;

        [HttpGet("{d1}/{d2}")]
        public Task<IList<tbLog>> GetAsync(string d1, string d2)
        {
            var rp = db.GetRepository<tbLog>();
            return rp.GetAllAsync(predicate: x => x.CreateDate >= DateTime.Parse(d1) && x.CreateDate <= DateTime.Parse(d2), disableTracking:true);
        }
    }
}
