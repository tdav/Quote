using QuoteServer.Database.Services;
using QuoteServer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuoteServer.v1.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [SwaggerTag("Филиаллардан излаш")]
    public class SearchOthersController : ControllerBase
    {
        private readonly ISearchDrugService sds;

        public SearchOthersController(ISearchDrugService _sds) => sds = _sds;


        [HttpGet("{id}")]
        public async Task<ActionResult<List<viDrugForKassa>>> Get(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var product = await sds.GetListAsync(id);
            return Ok(product);
        }
    }
}
