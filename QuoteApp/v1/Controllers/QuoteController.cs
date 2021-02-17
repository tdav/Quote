using Arch.EntityFrameworkCore.UnitOfWork;
using Swashbuckle.AspNetCore.Annotations;
using QuoteServer.Extensions.Controllers;
using Quote.Database.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Quote.Repository;

namespace QuoteServer.v1.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [SwaggerTag("Quote")]
    public class QuoteController : BaseController<tbQuote>
    {
        IUnitOfWork db;
        public QuoteController(IUnitOfWork _db, IMemoryCache _cache) : base(_db, _cache) 
        {
            db = _db;
        }

        [HttpGet("random")]
        public async Task<ActionResult> RandomQuote()
        {
            var rp = db.GetRepository<tbQuote>(true) as QuoteService;
            var res = await rp.RandomQuoteAsync();
            return Ok(res);
        }
    }
}
