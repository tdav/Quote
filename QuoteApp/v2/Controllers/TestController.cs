using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace QuoteServer.v2.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [SwaggerTag("Test")]
    [Route("v{version:apiVersion}/[controller]")]
    public class TestController : ControllerBase
    {

        [HttpGet("{ss}")]
        public ActionResult<string> GetTest(string ss)
        {
            var res = DateTime.Now.ToString() + ss;
            return Ok(res);
        }
    }
}
