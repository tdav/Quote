﻿using Microsoft.AspNetCore.Mvc;
using QuoteServer.Extensions.Controllers;
using Swashbuckle.AspNetCore.Annotations;
using Quote.Database.Models;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace QuoteServer.v2.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("2.0")]
    [SwaggerTag("Test")]
    [Route("v{version:apiVersion}/[controller]")]
    public class QuoteController : BaseControllerV2<tbQuote>
    {
        public QuoteController(IUnitOfWork _db, IMemoryCache _cache) : base(_db, _cache) { }


        [HttpGet("{ss}")]
        public ActionResult<string> GetTest(string ss)
        {
            var res = DateTime.Now.ToString() + ss;
            return Ok(res);
        }
    }
}
